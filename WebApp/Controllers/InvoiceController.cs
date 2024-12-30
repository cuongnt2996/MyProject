using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[Authorize]
public class InvoiceController:BaseController
{
    public async Task<IActionResult> Index(){
        int userId = Convert.ToInt32(User.FindFirstValue("UserId"));
        return View(await Provider.Invoice.GetInvoices(userId));
        // return Json(await Provider.Invoice.GetInvoices(userId));
    }
   
    public async Task<IActionResult> Detail(string InvoiceId){
        System.Console.WriteLine("****************************");
        System.Console.WriteLine(InvoiceId);
        return View(await Provider.Invoice.GetInvoice(InvoiceId));
    }
}