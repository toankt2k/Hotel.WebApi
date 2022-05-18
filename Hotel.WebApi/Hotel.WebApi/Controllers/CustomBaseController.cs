using Hotel.WebApi.core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web02.Core.Interfaces.Base;

namespace Hotel.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomBaseController<T> : ControllerBase
    {
        #region Fields
        private IBaseService<T> _baseService;
        #endregion
        #region Constructor
        public CustomBaseController(IBaseService<T> baseService)
        {
            this._baseService = baseService;
        }
        #endregion
        #region Api Controller
        /// <summary>
        /// Lấy tất cả bản ghi từ database
        /// </summary>
        /// <returns>
        /// trả về danh sách bản ghi lấy được
        /// trả về NoContent nếu k có dữ  liệu nào nhận được
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var result = _baseService.GetService();
                return Ok(result);
            }
            catch (CustomException ex)
            {
                var result = new {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        /// <summary>
        /// Lấy bản ghi bằng id của bản ghi
        /// </summary>
        /// <param name="id">id bản ghi được truyền qua router</param>
        /// <returns>
        /// bản ghi tìm được trong database
        /// NoContent nếu k có bản ghi nào
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpGet("{id}")]
        public virtual IActionResult GetById(Guid id)
        {
            try
            {
                var result = _baseService.GetByIdService(id);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                var result = new 
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng bản ghi được truyền từ form</param>
        /// <returns>
        /// trả về 201 nếu thêm thành công và k có lỗi
        /// trả về badrequest nếu các trường validate không hợp lệ
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpPost]
        public virtual IActionResult Post([FromBody] T entity)
        {
            try
            {
                var result = _baseService.InsertService(entity);
                return StatusCode(201, result);
            }
            catch (CustomException ex)
            {
                var result = new 
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        /// <summary>
        /// Thay đổi thông tin bản ghi
        /// </summary>
        /// <param name="id">mã id của bản ghi</param>
        /// <param name="entity">đối tượng bản ghi được truyền qua body</param>
        /// <returns>
        /// trả về Ok nếu sửa thành công
        /// trả về badrequest nếu người dùng thay đổi mã bản ghi hoặc không tìm thấy bản ghi để thay đổi
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpPut("{id}")]
        public virtual IActionResult Put([FromRoute] Guid id, [FromBody] T entity)
        {
            try
            {
                var result = _baseService.UpdateService(entity, id);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                var result = new 
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="id">mã id của bản ghi</param>
        /// <returns>
        /// xóa thành công: trả về code 200 và số dòng dc xóa
        /// xóa không thành công trả về lỗi badrequest (thường là do k tìm thấy bản ghi hoặc mã id gửi lên không đúng đinh dạng)
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            try
            {
                var result = _baseService.DeleteService(id);
                return Ok(result);

            }
            catch (CustomException ex1)
            {
                var result = new 
                {
                    devMsg = ex1.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex1.Data,
                };
                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                var result = new 
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="listId">mã id của bản ghi</param>
        /// <returns>
        /// xóa thành công: trả về code 200 và số dòng dc xóa
        /// xóa không thành công trả về lỗi badrequest (thường là do k tìm thấy bản ghi hoặc mã id gửi lên không đúng đinh dạng)
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpDelete("Multiple")]
        public virtual IActionResult MultiDelete([FromBody] List<Guid> listId)
        {
            try
            {
                var result = _baseService.MultiDelete(listId);
                return Ok(result);

            }
            catch (CustomException ex1)
            {
                var result = new 
                {
                    devMsg = ex1.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex1.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new 
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }

        }
        /// <summary>
        /// Filter, phân trang
        /// </summary>
        /// <returns>
        /// danh sách cá bản ghhi và tổng số trang, tổng số bản ghi
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (18/04/2022)
        [HttpGet("filter")]
        public virtual IActionResult Filter(int currentPage, int pageSize, string? filterText)
        {
            try
            {
                //trả về danh sách đã được filter và tổng số bản ghi
                //var result = _baseService.FilterService(currentPage, pageSize, filterText);
                return Ok();
            }
            catch (CustomException ex)
            {
                var result = new 
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ admin",
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        #endregion
    }
}
