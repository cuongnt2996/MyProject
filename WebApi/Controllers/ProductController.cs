using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController :BaseController
{
    // [HttpGet("all")]
    [HttpGet("{page}/{size}/{categoryId}/{keyword?}")]
    // public IEnumerable<Product> GetProducts(){
    //     return Provider.Product.GetProducts();
    // }
    public object GetProducts(int page = 1, int size =9, int categoryId =0, string keyword =""){
        int total = Provider.Product.CountProduct(page,size,categoryId,keyword);        
        return new{
            Products = Provider.Product.GetProductsAsync(page,size,categoryId,keyword),
            Pages = (total-1)/size+1
        };
    }
    [HttpGet("{id}")]
    public Product? GetProduct(int id){
        return Provider.Product.GetProduct(id);
    }
    [HttpPost]
    public int Add(Product obj){
        return Provider.Product.Add(obj);
    }
    [HttpPut("{id}")]
    public int Edit(int id,Product obj){
        string? imageUrl= Provider.Product.GetProductImageUrl(obj.ProductId);
        if(imageUrl != null && !imageUrl.Equals(obj.ImageUrl)){
            Helper.Delete(imageUrl);
        }
        return Provider.Product.Edit(obj);
    }
    [HttpDelete("{id}")]
    public int Delete(int id, int userId){
        return Provider.Product.Delete(id,userId);
    }
}