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
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public IEnumerable<Room> GetEmptyRooms()
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM Room AS r " +
                $"RIGHT Join BookedRoom as b" +
                $"on r.RoomId = b.RoomId" +
                $"WHERE b.CheckOut < now()";
            //dữ liệu trả về thông tin của đối tượng
            var data = sqlConnection.Query<Room>(sql: sqlCommand);
            //trả về kết quả
            return data;
        }
    }
}
