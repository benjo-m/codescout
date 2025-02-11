using api.Models;

namespace api.DTOs.User;

public class UserCreateRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Company { get; set; }
}