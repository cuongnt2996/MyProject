namespace WebApp.Models;
public class LoginModel{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool Remember { get; set; }
}