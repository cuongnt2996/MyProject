using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers;
public class ProductController:BaseController
{
    const int size =10;
    [HttpGet("/product/{action=index}/{page?}")]
    public async Task<IActionResult> Index(int page =1){
        ViewBag.CurrentPage = page;
         return View(await Provider.Product.GetProducts(page,size,0,""));
    }
    public async Task<IActionResult> Add(){
        ViewBag.Categories = new SelectList(await Provider.Category.GetCategories(),"CategoryId","CategoryName");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(Product obj){
        var userId = Convert.ToInt32(User.FindFirstValue("UserId"));
        obj.CreatedUserId = userId;
        var rs = await Provider.Product.Add(obj);
        if(rs > 0){
            TempData["msg"] = "Add new product success";
            return Redirect("/product");
        }
        ModelState.AddModelError("error","add new product failed");
        return View("/Product/Error");
    }
    public async Task<IActionResult> Edit(int id){
        Product? product = await Provider.Product.GetProductAsync(id);
        if(product is null){
            return Redirect("/product");
        }
        ViewBag.Categories = new SelectList(await Provider.Category.GetCategories(),"CategoryId","CategoryName");
        return View(product);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product obj){
        obj.ProductId =id;
        var rs = await Provider.Product.Edit(obj);
        if(rs > 0){
            return Redirect("/product");
        }
        // return await Edit();
        return Redirect("/product/Error");
    }
    public async Task<IActionResult> Details(int id){
        return View(await Provider.Product.GetProductAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id){
        var userId = Convert.ToInt32(User.FindFirstValue("UserId"));
        var rs = await Provider.Product.Delete(id,userId);
        // System.Console.WriteLine("***********************************************");
        // System.Console.WriteLine(userId);
        // System.Console.WriteLine(rs);
        if(rs>0){
            TempData["msg"]="Delete success";
        }else{
            ModelState.AddModelError("error","Delete failed");
        }
        return Redirect("/product");
    }
}