using Dapper;
using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Interfaces;
using MISA.Infrastructure.Respository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Respository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User GetByUserName(string userName)
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM User WHERE Username = @Username";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@Username", userName);
            //dữ liệu trả về thông tin của đối tượng
            var data = sqlConnection.Query<User>(sql: sqlCommand, param: dynamicParam).FirstOrDefault();
            //trả về kết quả
            return data;
        }
        public User CheckUsername(string userName)
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM User WHERE Username = @Username";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@Username", userName);
            //dữ liệu trả về thông tin của đối tượng
            var data = sqlConnection.Query<User>(sql: sqlCommand, param: dynamicParam).FirstOrDefault();
            //trả về kết quả
            return data;
        }

        public int Identify(User user)
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"Update User set Code = '', Active='1' WHERE Username = @Username and Code=@Code;";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@Username", user.Username);
            dynamicParam.Add($"@Code", user.IdentifyCode);
            //dữ liệu trả về thông tin của đối tượng
            var data = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam);
            //trả về kết quả
            return data;
        }

        public int Login(User user)
        {
            throw new NotImplementedException();
        }

        public int Register(User user)
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"Insert into User (UserId,Password,FullName,Position,Active,Code) values(@UserId,@Password,@FullName,@Position,0,@Code)";
            //khởi tạo tham số
            user.UserId = Guid.NewGuid();
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@Username", user.Username);
            dynamicParam.Add($"@Code", user.IdentifyCode);
            dynamicParam.Add($"@Password", user.Password);
            dynamicParam.Add($"@FullName", user.FullName);
            dynamicParam.Add($"@Position", user.Position);
            //dữ liệu trả về thông tin của đối tượng
            var data = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam);
            //trả về kết quả
            return data;
        }



    }
}
