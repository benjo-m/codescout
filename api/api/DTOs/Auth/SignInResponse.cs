using api.Models;

namespace api.DTOs.Auth;

public class SignInResponse
{
    public int? UserId { get; set; }
    public string? Token { get; set; }
    public DateTime TokenExpirationDate { get; set; }
}