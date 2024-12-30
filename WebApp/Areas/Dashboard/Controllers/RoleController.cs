using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard"),Authorize]
public class RoleController:BaseController
{
    public async Task<IActionResult> Index(){
        return View(await Provider.Role.GetRolesAsync());
    }
    public async Task<IActionResult> Edit(int id){
        return View(await Provider.Role.GetRoleAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Role obj){
        System.Console.WriteLine(id);
        System.Console.WriteLine(obj.RoleId);
        if(ModelState.IsValid){
            obj.RoleId = id;
            var rs = await Provider.Role.EditAsync(obj);
            if(rs >0 ){
                TempData["msg"] = "Edit Success!";
                return Redirect("/dashboard/Role");
            }
            ModelState.AddModelError("Error","Edit Failed");
            return Redirect("/dashboard/Role/Error");
        }
        ModelState.AddModelError("Error","Edit Failed");
        return Redirect("/dashboard/Role/Error");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id){
        Role? obj = await Provider.Role.GetRoleAsync(id);
        if(obj != null){
            var DeletedUserId = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (DeletedUserId != null && int.TryParse(DeletedUserId.Value, out int userId)) {
                obj.DeletedUserId = userId;
            }
            var rs = await Provider.Role.DeleteAsync(obj);
            System.Console.WriteLine(rs);
            if(rs>0){
                TempData["Msg"] ="Deleted";
                return Redirect("/dashboard/role");
            }
        }
        // System.Console.WriteLine(obj.DeletedUserId);

        ModelState.AddModelError("Error","Delete failed");
        return Redirect("/dashboard/role");
    }
    public IActionResult Add(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(Role obj){
        var UserId = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (UserId != null && int.TryParse(UserId.Value, out int userId)) {
             obj.CreatedUserId = userId;
        }
        var rs = await Provider.Role.Add(obj);
        if(rs > 0){
            TempData["msg"] = "Add new role success";
            return Redirect("/dashboard/role");
        }
        ModelState.AddModelError("Error","Add new role failed");
        return View(obj);        
    }
}