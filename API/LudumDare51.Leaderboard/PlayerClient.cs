using LudumDare51.Leaderboard.Models;
using RestSharp;

namespace LudumDare51.Leaderboard;

public class PlayerClient
{
    private readonly RestClient _restClient;
    private const string GetAllPlayer = "player";
    
    public PlayerClient(RestClient restClient)
    {
        _restClient = restClient;
    }

    public async Task<List<Player>> Get()
    {
        return await ExecuteRequest<List<Player>>(GetAllPlayer) ?? new List<Player>();
    }

    private async Task<T> ExecuteRequest<T>(string resource) where T : class
    {
        var request = new RestRequest(resource);
        var response = await _restClient.ExecuteAsync<T>(request);
        return response.IsSuccessful ? response.Data : null;
    }
}