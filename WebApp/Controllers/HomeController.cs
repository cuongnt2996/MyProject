using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        const int size =6;
        public async Task<IActionResult> Index(int page=1, int categoryId =0, string keyword ="")
        {   
            ViewBag.ActiveCategoryId = categoryId;       
            ViewBag.CurrentPage = page;
            // System.Console.WriteLine(categoryId);
            var categories = await Provider.Category.GetCategories();
            ViewBag.Categories = categories?.ToList() ?? new List<Category>();
            ProductPage? productPage = await Provider.Product.GetProducts(page,size,categoryId,keyword);
            return View(productPage);
       
        }
        public IActionResult Error(){
            return View();
        }
        public IActionResult Privacy(){
            return View();
        }
        public IActionResult Info(){
            return View();
        }
    }
    
}
