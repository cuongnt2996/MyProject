using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;
public class CartController:BaseController
{
    // const string CartCode ="cart";
    public async Task<IActionResult> Index(){
        var userId = Convert.ToInt32(User.FindFirstValue("UserId"));
        string? clientId = Request.Cookies["clientId"];
        // System.Console.WriteLine(clientId);
        if(string.IsNullOrEmpty(clientId)){
            clientId = Helper.RandomString(32);
            Response.Cookies.Append("clientId", clientId);
        }
        return View(await Provider.Cart.GetCarts(userId,clientId));
    }
    public async Task<int?> GetCartCount(){
        var userId = Convert.ToInt32(User.FindFirstValue("UserId"));
        string? clientId = Request.Cookies["clientId"];
        // System.Console.WriteLine(clientId);
        if(string.IsNullOrEmpty(clientId)){
            clientId = Helper.RandomString(32);
            Response.Cookies.Append("clientId", clientId);
        }
        IEnumerable<Cart>? carts = await Provider.Cart.GetCarts(userId,clientId);
        if(carts!=null){
            return carts.Count();
        }
        return 0;
    }
    [HttpPost]
    public async Task<int> Add(int quantity, int productId){
        string? clientId = Request.Cookies["clientId"];
        if(string.IsNullOrEmpty(clientId)){
            clientId = Helper.RandomString(32);
            Response.Cookies.Append("clientId", clientId);
        }
        Cart? cart = new Cart{
            Quantity = quantity,
            ProductId = productId,
            UserId = Convert.ToInt32(User.FindFirstValue("UserId")),
            ClientId = clientId
        };
        return await Provider.Cart.AddAsync(cart);
    }
}