namespace LudumDare51.API.Models.Request;

public class PlayerCreateRequest
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string ShirtColor { get; set; }
    public string PantColor { get; set; }
}