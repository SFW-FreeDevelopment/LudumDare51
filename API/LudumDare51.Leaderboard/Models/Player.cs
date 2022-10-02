namespace LudumDare51.Leaderboard.Models;

public class Player
{
    public string Id { get; set; }

    public string DisplayName { get; set; }

    public ushort Score { get; set; }
        
    public ushort Waves { get; set; }
}