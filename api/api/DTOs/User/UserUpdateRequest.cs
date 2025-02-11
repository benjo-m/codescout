namespace api.DTOs.User;

public class UserUpdateRequest
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public string? Biography { get; set; }
    public string? GitHub { get; set; }
    public string? LinkedIn { get; set; }
    public string? StackOverflow { get; set; }
    public string? X { get; set; }
    public string? Medium { get; set; }
}
