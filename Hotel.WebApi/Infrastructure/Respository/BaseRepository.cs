using Dapper;
using MISA.Web02.Core.Interfaces.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.WebApi.core.Attributies.CustomAttribute;

namespace MISA.Infrastructure.Respository
{
    /// <summary>
    /// kết nối database và thực thi các câu lệnh sql dùng chung cho các thực thể
    /// </summary>
    /// <typeparam name="T"> class truyền vào</typeparam>
    public class BaseRepository<T>:IBaseRepository<T>
    {
        #region Fields
        public string _sqlConnectionString;
        
        #endregion
        #region Constructor
        public BaseRepository()
        {
            
            _sqlConnectionString = "Host=140.238.38.197;Port=3306;Database=Hotel.WebApi;User Id=admin;Password=Anhtoankt12@";
            
        }
        #endregion
        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu trong bảng
        /// </summary>
        /// <returns>
        /// Danh sách dữ liệu
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual IEnumerable<T> Get()
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {entityName}";
            //dữ liệu trả về gồm các propperty của Employee
            //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
            var data = sqlConnection.Query<T>(sql: sqlCommand);
            //trả về kết quả
            return data;
        }

        /// <summary>
        /// Lấy mã đối tượng mới nhất trong hệ thống
        /// </summary>
        /// <returns>Mã đối tượng mới nhất trong hệ thống</returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual string GetNewCode()
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy entityName
            var entityName = typeof(T).Name;
            //sql query
            var sqlCommand = $"SELECT e.{entityName}Code FROM {entityName} as e";
            //Lấy danh sách code của entity:string
            var listEntityCode = sqlConnection.Query<string>(sql: sqlCommand);
            //danh sách code khi tách 2 chữ đầu
            var listCode = new List<int>();
            for (int i = 0; i < listEntityCode.Count(); i++)
            {
                var code = listEntityCode.ElementAt(i);
                listCode.Add(int.Parse(code.Substring(3, code.Length - 3)));
            }
            var newCode = $"{listCode.Max() + 1}";
            return newCode;
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// <returns>
        /// trả về số hàng bị ảnh hưởng
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int Insert(T entity)
        {
            //lấy tên của entity
            var entityName = typeof(T).Name;
            //lấy id mới nhất
            //lấy property Id của entity
            var propId = typeof(T).GetProperty($"{typeof(T).Name}Id");
            //set giá trị cho prop id
            propId.SetValue(entity, Guid.NewGuid());
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu chèn tên bảng vào
            var sqlCommand = $"Proc_Insert{entityName}";
            //truyền tham số cho Procedure
            var dynamicParam = new DynamicParameters();
            //lấy danh sách property của entity
            var listProperties = typeof(T).GetProperties();
            foreach(var prop in listProperties)
            {
                //xem prop có cho phép map vào database không
                var isNotMap = prop.IsDefined(typeof(NotMap), false);
                if (!isNotMap){
                    dynamicParam.Add($"@m_{prop.Name}", entity.GetType().GetProperty(prop.Name).GetValue(entity));
                }
            }
            var res = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
            //trả về kết quả
            return res;
        }

        /// <summary>
        /// cập nhật thông tin nhân viên
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns>số hàng bị ảnh hưởng</returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int Update(T entity)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //sử dụng proc update
            string sqlCommand = $"Proc_Update{entityName}";
            //truyền tham số cho Procedure
            var dynamicParam = new DynamicParameters();
            //lấy danh sách property của entity
            var listProperties = typeof(T).GetProperties();
            foreach (var prop in listProperties)
            {
                //xem prop có cho phép map vào database không
                var isNotMap = prop.IsDefined(typeof(NotMap), false);
                if (!isNotMap)
                {
                    //giá trị mới được truyền vào
                    var newVal = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                    dynamicParam.Add($"@m_{prop.Name}", newVal);
                }
            }
            //update dữ liệu
            var res = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
            //trả về kết quả
            return res;
        }

        /// <summary>
        /// Lấy nhân viên theo mã id
        /// </summary>
        /// <param name="id">mã id nhân viên</param>
        /// <returns>
        /// nhân viên tìm được/null
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual T GetById(Guid id)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {entityName} as e WHERE {entityName}Id = @{entityName}Id";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@{entityName}Id", id);
            //dữ liệu trả về gồm các propperty của T
            var data = sqlConnection.Query<T>(sql: sqlCommand, param: dynamicParam).FirstOrDefault();
            //trả về kết quả
            return data;
        }

        /// <summary>
        /// Xóa 1 bản ghi với id truyền vào
        /// </summary>
        /// <param name="id">mã id nhân viên</param>
        /// <returns>
        /// số hàng bị ảnh hưởng
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int Delete(Guid id)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"DELETE FROM {entityName} WHERE {entityName}Id = @{entityName}Id";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@{entityName}Id", id);
            //dữ liệu trả về gồm các propperty của Employee
            var data = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam);
            //trả về kết quả
            return data;
        }

        /// <summary>
        /// xóa nhiều bản ghi trong database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int MultiDelete(List<Guid> listId)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //ds nhân viên cần xóa
            var listParam = "";
            //khởi tạo tham số, sinh chuỗi tham số
            var dynamicParam = new DynamicParameters();
            //index
            var i = 0;
            foreach(var id in listId)
            {
                dynamicParam.Add($"@id{i}", id);
                listParam += $"@Id{i}" + ",";
                i++;
            }
            listParam = listParam.Substring(0, listParam.Length - 1).Trim();
            //lấy dữ liệu
            string sqlCommand = $"DELETE FROM {entityName} WHERE {entityName}Id IN ({listParam})";

            //dữ liệu trả về gồm các propperty của Employee
            var data = sqlConnection.Execute(sql: sqlCommand, param: dynamicParam);
            //trả về kết quả
            return data;
        }

        /// <summary>
        /// tìm bản ghi có code trùng vói code truyền vào
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual T FindByCode(string code)
        {
            var entityName = typeof(T).Name;
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlConnectionString);
            //lấy dữ liệu
            string sqlCommand = $"SELECT * FROM {entityName} WHERE {entityName}Code = @{entityName}Code";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add($"@{entityName}Code", code);
            //dữ liệu trả về thông tin của đối tượng
            var data = sqlConnection.Query<T>(sql: sqlCommand, param: dynamicParam).FirstOrDefault();
            //trả về kết quả
            return data;
        }
        #endregion
    }
}
