using IdentityAPI.Middleware;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountController(IUserService userRepository, IJwtBuilder jwtBuilder, IEncryptor encryptor)
        : ControllerBase
    {
        [HttpGet("get")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Get() =>
            new JsonResult(userRepository.GetAll());



        [HttpGet("get/{id}", Name = "lấy user bằng mail")]
        public ActionResult Get(string id)
        {
            var user = userRepository.Get(id);
            if (user == null)
                return NotFound();
            return new JsonResult(user);
        }

        [HttpPut("update/{id}")]
        public ActionResult Update(string id,[FromBody]User user)
        {
            var u = userRepository.Get(id);
            if (u == null)
                return NotFound();
            u.SetPassword(user.Password, encryptor);
            Update(u.Id, u);
            return Ok("sửa thành công");
        }

        [HttpDelete("detete/{id}")]
        public ActionResult Delete(string id)
        {
            var u = userRepository.Get(id);
            if (u == null)
                return NotFound();
            userRepository.RemoveAt(u);
            return Ok("xóa thành công");
        }
    }
}
