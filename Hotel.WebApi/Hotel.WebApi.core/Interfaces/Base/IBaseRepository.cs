using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>
        /// danh sách bản ghi
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public IEnumerable<T> Get();
        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>
        /// bản ghi lấy được
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public T GetById(Guid id);
        /// <summary>
        /// thêm mới bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// số hàng ảnh hưởng
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int Insert(T entity);
        /// <summary>
        /// thay đổi thông tin bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// số hàng bị ảnh hưởng
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int Update(T entity);
        /// <summary>
        /// xóa 1 bản ghi trong database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int Delete(Guid id);
        /// <summary>
        /// xóa nhiều bản ghi trong database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int MultiDelete(List<Guid> listId);
        /// <summary>
        /// lấy mã bản ghi mới nhất 
        /// </summary>
        /// <returns></returns>
        public string GetNewCode();
        /// <summary>
        /// Tìm bản ghi có mã bản ghi trùng với mã được truyền vào
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public T FindByCode(string entityCode);

    }
}
