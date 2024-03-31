using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Trả về một danh sách hoặc tài nguyên nào đó
            return Ok("Hello from API!");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Trả về tài nguyên có ID tương ứng
            return Ok($"Resource with ID {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] object data)
        {
            // Xử lý yêu cầu POST, data là dữ liệu được gửi từ client
            return Ok("Posted data successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] object data)
        {
            // Xử lý yêu cầu PUT để cập nhật tài nguyên có ID tương ứng với dữ liệu được gửi từ client
            return Ok($"Resource with ID {id} updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Xử lý yêu cầu DELETE để xóa tài nguyên có ID tương ứng
            return Ok($"Resource with ID {id} deleted successfully");
        }
    }
}
