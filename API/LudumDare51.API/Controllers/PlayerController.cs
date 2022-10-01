using LudumDare51.API.Models;
using LudumDare51.API.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace LudumDare51.API.Controllers
{
    [ApiController]
    [Route("player")]
    public class PlayerController : ControllerBase
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Player))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null)]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
        
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Player))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null)]
        [SwaggerResponse(StatusCodes.Status404NotFound, null)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return Ok();
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, null, typeof(Player))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null)]
        public async Task<IActionResult> Create([FromBody] PlayerCreateRequest request)
        {
            return Created(new Uri(""),null);
        }
    }
}