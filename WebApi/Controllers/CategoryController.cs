using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController :BaseController
{
    [HttpGet]
    public IEnumerable<Category> GetCategories(){
        return Provider.Category.GetCategories();
    }
    [HttpGet("{id}")]
    public int CheckDelete(int id){
        return Provider.Category.CheckDelete(id);
    }
    // [HttpGet("{id}")]
    // public Category GetCategory (int id){
    //     return Provider.Category.GetCategory(id);
    // }
    [HttpPost]
    public int Add(Category obj){
        return Provider.Category.Add(obj);
    }
    [HttpPut("{id}")]
    public int Edit(int id,Category obj){
        return Provider.Category.Edit(obj);
    }
    [HttpDelete("{id}")]
    public int Delete(int id, int userId){
        return Provider.Category.Delete(id,userId);
    }
    [HttpGet("keyword")]
    public IEnumerable<Category> GetCategoriesByProduct(string keyword){
        return Provider.Category.GetCategoriesByProduct(keyword);
    }
}