using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;
[ApiController,Route("api/[controller]")]
public class CartController:BaseController
{
    [HttpGet]
    public IEnumerable<Cart> GetCarts(int userId,string clientId){
        return Provider.Cart.GetCarts(userId,clientId);
    }
    [HttpPost]
    public int Add(Cart obj){
        return Provider.Cart.Add(obj);
    }
}