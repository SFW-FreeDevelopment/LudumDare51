using LudumDare51.API.Models.Request;

namespace LudumDare51.API.Models;

public class Player : BaseResource
{
    public string DisplayName { get; set; }
    
    public string ShirtColor { get; set; }
    public string PantColor { get; set; }
    
    public int TimesPlayed { get; set; }
    
    public int Score { get; set; }
    
    public int Waves { get; set; }

    public Player() { }

    public Player(PlayerCreateRequest playerCreateRequest)
    {
        Id = playerCreateRequest.Id;
        DisplayName = playerCreateRequest.DisplayName;
        ShirtColor = playerCreateRequest.ShirtColor;
        PantColor = playerCreateRequest.PantColor;
    }
}