using IdentityAPI.Middleware;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class IdentityController
      (IUserService userRepository, IJwtBuilder jwtBuilder, IEncryptor encryptor)
        : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user,
               [FromQuery(Name = "d")] string destination = "frontend")
        {
            var u = userRepository.Get(user.Email);

            if (u == null)
            {
                return NotFound("Không tìm thấy người dùng.");
            }

            if (destination == "backend" && !u.IsAdmin)
            {
                return BadRequest("Không thể xác thực người dùng.");
            }

            var isValid = u.ValidatePassword(user.Password, encryptor);

            if (!isValid)
            {
                return BadRequest("Không thể xác thực người dùng.");
            }

            var token = jwtBuilder.GetToken(u.Id);
            return Ok(token);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var u = userRepository.Get(user.Email);

            if (u != null)
            {
                return BadRequest("Người dùng đã tồn tại.");
            }

            user.SetPassword(user.Password, encryptor);
            userRepository.Insert(user);

            return Ok();
        }

        [HttpGet("validate")]
        public IActionResult Validate([FromQuery(Name = "email")] string email,
                                      [FromQuery(Name = "token")] string token)
        {
            var u = userRepository.Get(email);

            if (u == null)
            {
                return NotFound("Không tìm thấy người dùng.");
            }

            var userId = jwtBuilder.ValidateToken(token);

            if (userId != u.Id)
            {
                return BadRequest("Mã không hợp lệ.");
            }

            return Ok(userId);
        }


    }
}