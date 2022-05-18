using Hotel.WebApi.core.Exceptions;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Hotel.WebApi.core.Attributies.CustomAttribute;

namespace MISA.Web02.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        #region Fields
        private IBaseRepository<T> _baseRepository;
        #endregion
        #region Constructor
        public BaseService(IBaseRepository<T> tiem)
        {
            this._baseRepository = tiem;
        }
        #endregion
        /// <summary>
        /// xóa 1 bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int DeleteService(Guid entityId)
        {
            var entityName = typeof(T).Name;
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //kiểm tra bản ghi đã tồn tại
            var entityInDatabase = _baseRepository.GetById(entityId);
            if (entityInDatabase == null)//nếu bản ghi không tồn tại tức là bản ghi đã bị xóa trước đó
            {
                //thêm lỗi vào errorMsg
                errorMsg.Add($"{entityName}Empty", "Không tìm thấy đối tượng");
            }
            if (errorMsg.Count() > 0)
            {
                throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
            }
            var result = _baseRepository.Delete(entityId);
            return result;

        }
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int InsertService(T entity)
        {
            var entityName = typeof(T).Name;
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(entity);
            if (validateEmptyResult.Count() > 0)
            {
                throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
            }
            //lấy code của entity truyền vào
            var code = typeof(T).GetProperty($"{entityName}Code").GetValue(entity).ToString();
            //kiểm tra mã code trùng
            var data = _baseRepository.FindByCode(code);
            //nếu có bản ghi trùng với mã hiện tại
            if (data != null)
            {
                errorMsg.Add($"{entityName}Code", "Mã bị trùng");
            }
            if (errorMsg.Count() > 0)//nếu danh sách lỗi có lỗi thì throw exception
            {
                throw new CustomException("Dữ liệu không hợp lệ" , errorMsg);
            }
            //thêm mới database
            var result = _baseRepository.Insert(entity);
            return result;
        }
        /// <summary>
        /// sửa 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán - MF1095 (14/04/2022)
        public virtual int UpdateService(T entity, Guid id)
        {
            var entityName = typeof(T).Name;
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(entity);
            if (validateEmptyResult.Count() > 0)//nếu dữ liệu bị trống
            {
                throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
            }
            //lấy code của entity truyền vào
            var newCode = typeof(T).GetProperty($"{entityName}Code").GetValue(entity).ToString();
            //lấy giá trị cũ trong data base
            var oldEntity = _baseRepository.GetById(id);
            //nếu không tìm thấy bản ghi / bản ghi đã bị xóa trước khi sửa 
            if (oldEntity == null)
            {
                errorMsg.Add($"{entityName}NotFound", "Không tìm thấy đối tượng");
                throw new CustomException("Không tìm thấy đôi tượng cần sửa", errorMsg);
            }
            else
            {
                //lấy mã code của đối tượng đã có trong database
                var oldCode = typeof(T).GetProperty($"{entityName}Code").GetValue(oldEntity).ToString();
                if (oldCode == newCode)//nếu bằng mã mới có nghĩa mã code đã tồn tại
                {
                    //thêm 1 lỗi vào danh sách lỗi
                    errorMsg.Add($"{entityName}Code", "Mã Bị trùng");
                    //throw exception
                    throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
                }
            }
            if (errorMsg.Count() > 0)
            {
                throw new CustomException("Dữ liệu không hợp lệ", errorMsg);
            }
            //set lại data
            //lấy các prop trong entity
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                //lấy giá trị của entity đó
                var entityValue = prop.GetValue(entity);
                //lấy giá trị của data
                var dataValue = prop.GetValue(entity);
                //nếu các trường không được nhập vào thì người dùng không muốn thay đổi
                //nên giữ nguyên giá trị cũ trong database
                if (entityValue == null)
                {
                    prop.SetValue(entity, dataValue);
                }
            }
            // gọi update đến repository
            var result = _baseRepository.Update(entity);
            return result;
        }

        /// <summary>
        /// xóa nhiều bản ghi trong bảng
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(15/04/2022)
        public virtual int MultiDelete(List<Guid> listId)
        {
            var result = _baseRepository.MultiDelete(listId);
            return result;
        }

        /// <summary>
        /// kiểm tra các trường required bị trống
        /// </summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(15/04/2022)
        protected Dictionary<string, string> ValidateEmpty(T entity)
        {
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //kiểm tra các trường required có bị null không
            //lấy các prop trong entity
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                //kiểm tra xem có được định nghĩa là required không
                var isRequired = prop.IsDefined(typeof(Required), false);
                if (isRequired)
                {
                    //lấy giá trị của prop đó
                    var propValue = prop.GetValue(entity);
                    //kiểm tra propValue null
                    if (propValue == null)
                    {
                        //lấy các thuộc tính được hiển thị hiển thị của prop
                        var displayAttribute = (Required)Attribute.GetCustomAttribute(prop, typeof(Required));
                        errorMsg.Add($"{prop.Name}", $"{displayAttribute.Msg} không được để trống.");

                    }
                    //kiểm tra là chuỗi rỗng
                    else if (string.IsNullOrEmpty(propValue.ToString()))
                    {
                        //lấy các thuộc tính được hiển thị hiển thị của prop
                        var displayName = (Required)Attribute.GetCustomAttribute(prop, typeof(Required));
                        errorMsg.Add($"{prop.Name}", $"{displayName.Msg} không được để trống");

                    }
                }
            }
            return errorMsg;
        }

        public virtual IEnumerable<T> GetService()
        {
            var result = _baseRepository.Get();
            return result;
        }

        public virtual T GetByIdService(Guid id)
        {
            var result = _baseRepository.GetById(id);
            return result;
        }
        public virtual string GetNewCodeService()
        {
            var result = _baseRepository.GetNewCode();
            return result;
        }

    }
}
