using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApp.Controllers;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard"),Authorize]
public class UserController:BaseController
{
    [Authorize]
    public async Task<IActionResult> Index(){
        string? token = User.FindFirstValue(ClaimTypes.Authentication);
        System.Console.WriteLine(token);
        if(!string.IsNullOrEmpty(token))
        {
            return View(await Provider.User.GetUsersAsync(token));
        }
        return View();
    }
    public async Task<IActionResult> Edit(int id){
        ViewBag.Roles = new SelectList(await Provider.Role.GetRolesAsync(),"RoleId", "RoleName");
        return View(await Provider.User.GetUserAsync(id.ToString()));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(User obj){
        // string jsonObj = JsonConvert.SerializeObject(obj, Formatting.Indented); 
        // System.Console.WriteLine("JSON của đối tượng obj:"); 
        // System.Console.WriteLine(jsonObj);
        if(ModelState.IsValid){
            var rs = await Provider.User.Edit(obj);
            if(rs>0){
                return Redirect("/dashboard/User");
            }
        }
        return await Edit(obj.UserId);
        // return Redirect("/Dashboard/User/Error");
    }
    public IActionResult AddUser(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddUser(RegisterModel obj){
        var ret = await Provider.User.CheckExistUser(obj.UserName,obj.Email);
        if(ret > 0){
            ModelState.AddModelError("Error","Username or Email already exists" );
            return View(obj);
        }
        var salt = Helper.RandomString(32);
        User user = new User{
            UserName = obj.UserName,
            Email = obj.Email,
            PasswordSalt = salt,
            Password = Helper.HashPassword(obj.Password, salt),
            FirstName = obj.FirstName,
            LastName = obj.LastName,
            DateOfBirth = obj.DateOfBirth
        };
        var rs = await Provider.User.Add(user);
        if(rs > 0){
            TempData["msg"] = "Register success";
            return Redirect("/Home");
        }
        ModelState.AddModelError("Error","Register failed" );
        return View(obj);
    }
}