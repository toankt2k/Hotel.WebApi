using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Exceptions;
using Hotel.WebApi.core.Interfaces;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.WebApi.core.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Identify(User user)
        {
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(user);
            if (validateEmptyResult.Count() > 0)//nếu dữ liệu bị trống
            {
                throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
            }
            //lấy code của entity truyền vào
            //lấy giá trị cũ trong data base
            var oldEntity = _userRepository.GetByUserName(user.Username);
            //nếu không tìm thấy bản ghi / bản ghi đã bị xóa trước khi sửa 
            if (oldEntity == null)
            {
                errorMsg.Add($"UserNotFound", "Không tìm thấy đối tượng");
                throw new CustomException("Không tìm thấy đôi tượng cần sửa", errorMsg);
            }
            //nếu không đúng với mã xác nhận thì lỗi 
            if (oldEntity.Code != user.Code)
            {
                errorMsg.Add($"NotIdentify", "Mã xác nhận không hợp lệ, vui lòng kiểm tra lại");
                throw new CustomException("Mã xác thực không đúng", errorMsg);
            }
            if (errorMsg.Count() > 0)
            {
                throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
            }
            //set lại data
            //lấy các prop trong entity
            var props = user.GetType().GetProperties();
            foreach (var prop in props)
            {
                //lấy giá trị của entity đó
                var entityValue = prop.GetValue(user);
                //lấy giá trị của data
                var dataValue = prop.GetValue(user);
                //nếu các trường không được nhập vào thì người dùng không muốn thay đổi
                //nên giữ nguyên giá trị cũ trong database
                if (entityValue == null)
                {
                    prop.SetValue(user, dataValue);
                }
            }
            user.Active = 1;
            user.Code = "";
            // gọi update đến repository
            var result = _userRepository.Update(user);
            return result == 1;
        }

        public void Login(User user)
        {

        }

        public bool RegisterAsync(User user)
        {
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(user);
            if (validateEmptyResult.Count() > 0)
            {
                throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
            }
            if (errorMsg.Count() > 0)//nếu danh sách lỗi có lỗi thì throw exception
            {
                throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
            }
            var oldUser = _userRepository.GetByUserName(user.Username);
            if (oldUser != null)//nếu danh sách lỗi có lỗi thì throw exception
            {
                throw new CustomException("Đăng ký không thành công, tài khoản đã tồn tại", errorMsg);
            }
            user.Active = 0;
            user.Code = RandomString(6);
            var res = SendCode(user);
            _userRepository.Insert(user);
            return true;
        }
        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private async static Task<object> SendCode(User user)
        {
            String to = user.Email;
            String from = "toankt2k@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Mã xác thực tài khoản";
            message.Body = $"Kính chào: {user.FullName}!\n" +
            "TENTEN chân thành cảm ơn Quý khách đã tin tưởng và đăng ký sử dụng dịch vụ của chúng tôi.\n" +
            "Hệ thống đã tiếp nhận yêu cầu đăng ký cho Tài khoản quản lý tên miền từ quý khách.\n" +
            "Thông tin tài khoản như sau:\n" +
            $"Tài khoản: {user.Username}\n" +
            $"Mật khẩu: {user.Password}\n" +
            $"Mã xác nhân: {user.Code} Vui lòng xác nhận để kích hoạt tài khoản";

            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(from));
            message.Sender = new MailAddress(to);

            // Tạo SmtpClient kết nối đến smtp.gmail.com
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.Credentials = new NetworkCredential(from, "0973590850");
                client.EnableSsl = true;
                return await SendMail(from, to, message.Subject, message.Body, client);
            }
        }
        private static async Task<bool> SendMail(string _from, string _to, string _subject, string _body, SmtpClient client)
        {
            // Tạo nội dung Email
            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);


            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
