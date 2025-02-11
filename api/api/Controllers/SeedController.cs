using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        public SeedData _data;

        public SeedController(SeedData data)
        {
            _data = data;
        }

        [HttpGet]
        public void Generate()
        {
            _data.GenerateData();
        }

        [HttpPost("GenerateMessages")]
        public void GenerateMessages()
        {
            _data.GenerateMessages();
        }
    }
}
