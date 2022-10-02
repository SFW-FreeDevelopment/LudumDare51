using LudumDare51.API.Database.Repositories;
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
        private readonly PlayerRepository _playerRepository;
        
        public PlayerController(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(List<Player>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _playerRepository.Get());
        }
        
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Player))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null)]
        [SwaggerResponse(StatusCodes.Status404NotFound, null)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return Ok(await _playerRepository.Get(id));
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, null, typeof(Player))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null)]
        public async Task<IActionResult> Create([FromBody] PlayerCreateRequest request)
        {
            var player = await _playerRepository.Create(new Player(request));
            return Created($"player/{player.Id}", player);
        }
    }
}