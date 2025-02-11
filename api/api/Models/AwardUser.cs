using System.Text.Json.Serialization;

namespace api.Models;

public class AwardUser
{
    public int AwardId { get; set; }
    public Award Award { get; set; }
    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }
}