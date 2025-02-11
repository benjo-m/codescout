namespace api.DTOs.Auth;

public class TwoFactorRequest
{
    public int UserId { get; set; }
    public string? Otp { get; set; }
}
