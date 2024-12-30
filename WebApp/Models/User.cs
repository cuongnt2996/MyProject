namespace WebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } =null!;
        public string Password { get; set; } =null!;
        public string PasswordSalt { get; set; }  =null!;
        public string Email { get; set; } =null!;
        public int RoleId { get; set; }
        public string? RoleCode { get; set; }
        public string? RoleName { get; set; }
        public string FirstName { get; set; } =null!;
        public string LastName { get; set; } =null!;
        public DateTime DateOfBirth { get; set; }
        
    }
}
