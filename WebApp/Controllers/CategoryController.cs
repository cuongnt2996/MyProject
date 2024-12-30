using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class CategoryController:BaseController
{
    public async Task<IActionResult> Index(){
        return View(await Provider.Category.GetCategories());
    }
    public IActionResult Add(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(Category obj){
        var ret = await Provider.Category.Add(obj);
        if(ret >0 ){
            TempData["msg"]="Add category success!";
            return Redirect("/category");
        }
        ModelState.AddModelError("error","Add category failed");
        return View(obj);
    }
    [HttpGet]
    public async Task<int> CheckDelete(int id){
        return await Provider.Category.CheckDelete(id);
    }
    public async Task<IActionResult> Edit(int id){
        return View( await Provider.Category.GetCategoryAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Category obj){
        obj.CategoryId = id;
        var rs =  await Provider.Category.Edit(obj);
        if(rs > 0){
            TempData["msg"] ="Edit success";
            return Redirect("/Category");
        }
        ModelState.AddModelError("error","Edit failed");
        return View(obj);
    }
}