using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Exceptions;
using Hotel.WebApi.core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MISA.Web02.Core.Interfaces.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Hotel.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : CustomBaseController<User>
    {
        private IUserService _userService;

        private IConfiguration _configuration;
        public UsersController(IUserService userService, IConfiguration config) : base(userService)
        {
            _userService = userService;
            _configuration = config;
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
        [HttpPost("identify")]
        public IActionResult Identify([FromBody] User user)
        {
            try
            {
                var res = _userService.Identify(user);
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
