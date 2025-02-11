namespace api.Models;

public class Otp
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Code { get; set; }
}
