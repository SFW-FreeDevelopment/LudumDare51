using LudumDare51.Leaderboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace LudumDare51.Leaderboard.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly PlayerClient _playerClient;

    public PlayerController(ILogger<WeatherForecastController> logger, PlayerClient playerClient)
    {
        _logger = logger;
        _playerClient = playerClient;
    }

    [HttpGet]
    public async Task<IEnumerable<Player>> Get()
    {
        return await _playerClient.Get();
    }
}