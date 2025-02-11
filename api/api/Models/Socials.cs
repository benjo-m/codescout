using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace api.Models;

public class Socials
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }

    public string? GitHub { get; set; }
    public string? LinkedIn { get; set; }
    public string? StackOverflow { get; set; }
    public string? X { get; set; }
    public string? Medium { get; set; }

    public void UpdateLinks(string? gh, string? li, string? so, string? x, string? m)
    {
        GitHub = string.IsNullOrEmpty(gh) ? null : gh;
        LinkedIn = string.IsNullOrEmpty(li) ? null : li;
        StackOverflow = string.IsNullOrEmpty(so) ? null : so;
        X = string.IsNullOrEmpty(x) ? null : x;
        Medium = string.IsNullOrEmpty(m) ? null : m;
    }
}