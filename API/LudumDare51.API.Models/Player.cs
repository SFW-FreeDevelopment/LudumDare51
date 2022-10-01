namespace LudumDare51.API.Models;

public class Player : BaseResource
{
    public string DisplayName { get; set; }
    public ushort Wins { get; set; }
    public ushort Losses { get; set; }
    public string ShirtColor { get; set; }
    public string PaintColor { get; set; }
    public ushort TimesPlayed { get; set; }
}