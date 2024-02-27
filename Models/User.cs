namespace authorization_service.Models;

public class User {
    public string Id { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}