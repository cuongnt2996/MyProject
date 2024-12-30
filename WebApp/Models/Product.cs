namespace WebApp.Models;
public class Product
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }=null!;
    public string Description { get; set; } =null!;
    public string Unit { get; set; } =null!;
    public decimal Price { get; set; }
    public bool IsActived { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public string? ImageUrl { get; set; } 
    public int CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; } =DateTime.Now;
    public int? DeletedUserId { get; set; }
    public DateTime? DeletedDate { get; set; }


}