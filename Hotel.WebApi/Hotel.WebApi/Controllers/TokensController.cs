using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Exceptions;
using Hotel.WebApi.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {

        private IUserService _userService;

        private IConfiguration _configuration;
        public TokensController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _configuration = config;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var newUser = _userService.Login(user);
                return Ok(GetToken(user));
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
        private string GetToken(User user)
        {

            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("username", user.Username),
                        new Claim("userfullname", user.FullName),

                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            //trả về đối tượng là token và thông tin account
            var data = new JwtSecurityTokenHandler().WriteToken(token);
            return data;
        }
    }
}
