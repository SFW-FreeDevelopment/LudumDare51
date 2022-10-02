using LudumDare51.API.Models.Request;

namespace LudumDare51.API.Models;

public class Player : BaseResource
{
    public string DisplayName { get; set; }
    
    public ushort Wins { get; set; }
    
    public ushort Losses { get; set; }
    public string ShirtColor { get; set; }
    public string PantColor { get; set; }
    
    public ushort TimesPlayed { get; set; }
    
    public ushort Score { get; set; }
    
    public ushort Waves { get; set; }

    public Player() { }

    public Player(PlayerCreateRequest playerCreateRequest)
    {
        DisplayName = playerCreateRequest.DisplayName;
        ShirtColor = playerCreateRequest.ShirtColor;
        PantColor = playerCreateRequest.PantColor;
    }
}