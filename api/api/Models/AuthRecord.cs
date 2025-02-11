namespace api.Models;

public class AuthRecord
{
    public int Id { get; set; }
    public User? User { get; set; }
    public string? Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}