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
            string sqlCommand = $"SELECT * FROM User WHERE UserName = @UserName";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@UserName", userName);
            //dữ liệu trả về thông tin của đối tượng
            var data = sqlConnection.Query<User>(sql: sqlCommand, param: dynamicParam).FirstOrDefault();
            //trả về kết quả
            return data;
        }

        public int Login(User user)
        {
            throw new NotImplementedException();
        }

        public int Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
