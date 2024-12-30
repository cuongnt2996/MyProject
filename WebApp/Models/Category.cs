namespace WebApp.Models;
public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } =null!;
    public bool IsActived { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    // public string? ImageUrl { get; set; } 
    public int? CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; } =DateTime.Now;
    public int? DeletedUserId { get; set; }
    public DateTime? DeletedDate { get; set; }
}