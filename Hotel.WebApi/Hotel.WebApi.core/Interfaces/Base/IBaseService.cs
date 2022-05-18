using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Base
{
    /// <summary>
    /// cũng cấp các interface xử lý các nghiệp vụ cho các dihcj vụ
    /// </summary>
    public interface IBaseService<T>
    {
        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetService();
        /// <summary>
        /// lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetByIdService(Guid id);
        /// <summary>
        /// lất mã code mới nhất
        /// </summary>
        /// <returns></returns>
        public string GetNewCodeService();
        /// <summary>
        /// thêm mới bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int InsertService(T entity);
        /// <summary>
        /// sửa bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int UpdateService(T entity, Guid id);
        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int DeleteService(Guid entityId);
        /// <summary>
        /// xoas nhiều bản ghi trong bảng
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public int MultiDelete(List<Guid> listId);
    }
}
