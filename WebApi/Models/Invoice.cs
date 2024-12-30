namespace WebApi.Models;
public class Invoice
{
    public string InvoiceId { get; set; } =null!;
    public string? InvoiceCode { get; set; }
    public int CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CustomerName { get; set; } =null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; }=null!;
    public string Phone { get; set; }=null!;
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public List<InvoiceItem> InvoiceItems { get; set; }=null!;
}