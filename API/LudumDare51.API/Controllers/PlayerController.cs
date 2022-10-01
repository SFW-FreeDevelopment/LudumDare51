using LudumDare51.API.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace LudumDare51.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] string id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] PlayerCreateRequest request)
        {
            return Created(new Uri(""),null);
        }
    }
}