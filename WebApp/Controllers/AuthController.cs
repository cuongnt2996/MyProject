using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;
public class AuthController:BaseController
{
    public IActionResult Login(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel obj, string returnUrl){
        System.Console.WriteLine();
        string? token = await Provider.User.LoginAsync(obj);
        // var member = await Provider.User.GetUserAsync(obj.UserName);
        System.Console.WriteLine(token);
        if(string.IsNullOrEmpty(token)){
            ModelState.AddModelError("Error","Login failed");
            return View(obj);
        }
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        var securityToken = handler.ReadJwtToken(token);

        // if(Helper.HashPassword(obj.Password, member.PasswordSalt) != member.Password){
        //     ModelState.AddModelError("Error","Password is incorrect");
        //     return View(obj);           
        // }
        List<Claim> claims = new List<Claim>(securityToken.Claims);
        claims.Add(new Claim(ClaimTypes.Authentication,token));
        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties{IsPersistent = obj.Remember});
        if(Url.IsLocalUrl(returnUrl)){
            return Redirect(returnUrl);
        }
        return Redirect("/home");
        
    }
    public IActionResult Register(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel obj){
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
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/Auth/Login");
    }


}