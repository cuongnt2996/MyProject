namespace WebApi.Models;
public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } =null!;
    public bool IsActived { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public int? CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; } =DateTime.Now;
    public int? DeletedUserId { get; set; }
    public DateTime? DeletedDate { get; set; }
}