namespace WebApi.Models;
public class InvoiceItem
{
    public int InvoiceItemId { get; set; }  
    public string InvoiceId { get; set; }=null!;
    public int CartId { get; set; } 
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; } 
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}