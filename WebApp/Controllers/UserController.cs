using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[Authorize]
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
        return View(await Provider.User.GetUserAsync(id.ToString()));
    }
}