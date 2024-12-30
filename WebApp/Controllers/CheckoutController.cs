using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Services;

namespace WebApp.Models;
[Authorize]
public class CheckoutController:BaseController
{
    public async Task<IActionResult> Invoice(){
        var userId = Convert.ToInt32(User.FindFirstValue("UserId"));
        string? clientId = Request.Cookies["clientId"];
        // System.Console.WriteLine(clientId);
        if(string.IsNullOrEmpty(clientId)){
            clientId = Helper.RandomString(32);
            Response.Cookies.Append("clientId", clientId);
        }
        IEnumerable<Cart>? carts = await Provider.Cart.GetCarts(userId,clientId);
        List<InvoiceItem> invoices = carts!.Select(Cart => new InvoiceItem{
            CartId = Cart.CartId,
            ProductId = Cart.ProductId,
            ProductName = Cart.ProductName,
            Quantity = Cart.Quantity,
            Price = Cart.Price,
            ImageUrl = Cart.ImageUrl
        }).ToList();
        Invoice invoice =  new Invoice {
            CreatedUserId = Convert.ToInt32(User.FindFirstValue("UserId")),
            CustomerName= User.FindFirstValue(ClaimTypes.Name)!,
            Email = User.FindFirstValue(ClaimTypes.Email)!,
            Amount = invoices.Sum(i => i.Quantity*i.Price),
            InvoiceItems = invoices
        };
        return View(invoice);
    }
    [HttpPost]
    public async Task<IActionResult> Invoice(Invoice obj) {
        var  invoiceId = Helper.RandomString(32);
        obj.InvoiceId = invoiceId;
        obj.CreatedDate = DateTime.Now;
        foreach(var item in obj.InvoiceItems){
            item.InvoiceId = invoiceId;
        }
        string? url = await Provider.Invoice.Add(obj);
        System.Console.WriteLine(url);
        if(!string.IsNullOrEmpty(url)){
            
            return Redirect(url);
        }
        return Redirect("/checkout/Error");
    }
    public async Task<IActionResult> BackVnPayment(VnPaymentResponse obj){
        if(obj != null){
            var rs =await Provider.Invoice.PaymentResponse(obj);
            if(obj.TransactionStatus =="00"){
                await Provider.Invoice.Paid(obj.TxnRef);
            }
            return Redirect("/invoice");
        }
        return Redirect("/checkout/error");
        // return Json(obj);
    }
}