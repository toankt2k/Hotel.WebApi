using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Exceptions;
using Hotel.WebApi.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web02.Core.Interfaces.Base;
using System.Net;
using System.Net.Mail;

namespace Hotel.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController<User>
    {
        private IUserService _userService;

        public UsersController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                return Ok();
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                var res = _userService.RegisterAsync(user);
                return Ok(res);
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        

    }
}
