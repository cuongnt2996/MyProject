namespace WebApp.Models;
public class Cart
{
    public int CartId { get; set; }
    public string? CategoryName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; } 
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int? UserId { get; set; }
    public string? ClientId { get; set; }//dùng trong trường hợp user chưa đăng nhập
}