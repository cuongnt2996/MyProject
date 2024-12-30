using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using System.Text.Json;
using WebApi.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController :BaseController
{
    IConfiguration configuration;
    public UserController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    [HttpPost]
    public int Add(User obj){
        // var passwordSalt = Helper.RandomString(32);
        // var password = Helper.HashPassword(obj.Password, passwordSalt);
        // obj.PasswordSalt = passwordSalt;
        // obj.Password = password;
        return Provider.User.Add(obj);
    }
    [Authorize]
    [HttpGet("all")]
    public IEnumerable<User> GetUsers(){
        return Provider.User.GetUsers();
    }
    [HttpGet("{identifier}")]
    public User? GetUser(string identifier){
        int id;
        if(int.TryParse(identifier, out id)){
            return Provider.User.GetUserById(id);
        }
        return Provider.User.GetUser(identifier);
    }
    // [HttpGet("{id}")]
    // public User? GetUserById(int id){
    //     return Provider.User.GetUserById(id);
    // }
    [HttpGet("{userName}/{email}")]
    public int? CheckExistUser(string userName,string email){
        var rs = Provider.User.CheckExistsUser(userName,email);
        return rs;
    }
    [HttpPost("login")]
    public string? Login(LoginModel obj){
        string? Salt = Provider.User.GetUser(obj.UserName)?.PasswordSalt;
        if(Salt is null){
            return null;
        }
        obj.Password = Helper.HashPassword(obj.Password, Salt);
        User? user = Provider.User.Login(obj);
        if(user !=  null ){
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim("UserId", user.UserId.ToString())
            };
            string name = user.FirstName;
            if(user.LastName != null){
                name = name + " " +user.LastName;
                claims.Add(new Claim(ClaimTypes.Surname,user.LastName));
            }
            claims.Add(new Claim(ClaimTypes.Name, name));
            string? secretKey = configuration["JWt:SecretKey"];
            if(secretKey !=null){
                return Helper.GenerateToken(claims, secretKey);
            }
        }
        return null;
    }
    [HttpPut]
    public int Edit(User obj){
        return Provider.User.Edit(obj);
    }
    // [HttpGet("{userName}/{password}")]
    // public User? GetUser(string userName, string password){
    //     var obj = Provider.User.GetUser(userName);
    //     if(obj != null){
    //         return Provider.User.GetUser(userName, Helper.HashPassword(password,obj.PasswordSalt));
    //     }
    //     return null;
    // }

}