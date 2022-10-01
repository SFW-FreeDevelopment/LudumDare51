using System.ComponentModel.DataAnnotations;

namespace LudumDare51.API.Models;

public class BaseResource
{
    public string Id { get; set; }

    [ConcurrencyCheck]
    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    public string GameName { get; set; }
}