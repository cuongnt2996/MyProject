namespace WebApp.Models;
public class ProductPage
{
    public IEnumerable<Product> Products {set; get;} =null!;
    public int Pages { get; set; }

}