namespace WebApp.Models;
public class Role
{
    public int RoleId  { get; set; }
    public string RoleCode { get; set; } =null!;
    public string RoleName { get; set; } =null!;
    public bool IsActived { get; set; }
    public bool IsDeleted { get; set; }
    public int CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? DeletedUserId { get; set; }
    public DateTime? DeletedDate { get; set; }
}