namespace WebApp.Areas.Dashboard.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
[Area("dashboard")]
public class HomeController:BaseController
{
    public IActionResult Index(){
        return View();
    }
}