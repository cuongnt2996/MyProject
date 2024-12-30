using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
[Route("search")]
public class SearchController:BaseController
{
    const int size =6;
    [HttpGet("index")]
    public async Task<IActionResult> Index(int page = 1, int categoryId = 0, string keyword = "")
    {
        // System.Console.WriteLine(keyword);
        ViewBag.ActiveCategoryId = categoryId;
        ViewBag.CurrentPage = page;
        var categories = await Provider.Category.GetCategoriesByProduct(keyword);
        ViewBag.Categories = categories?.ToList() ?? new List<CategoryWithQuantity>();
        if(categories != null){
            ViewBag.ProductCount = categories.Sum(p=>p.ProductQuantity);
        }else{
            ViewBag.ProductCount = 0;
        }
       
        ViewData["Keyword"] = keyword;
        ProductPage? productPage = await Provider.Product.GetProducts(page, size, categoryId, keyword);
        // if(categories != null && productPage!=null)
        // {
            
        //     var filterCategories = categories.Where(c=> productPage.Products.Any(p => c.CategoryId == p.CategoryId)).ToList();
        //     ViewBag.Categories = filterCategories?.ToList() ?? new List<CategoryWithQuantity>();
        // }
        
        return View(productPage);
    }
}