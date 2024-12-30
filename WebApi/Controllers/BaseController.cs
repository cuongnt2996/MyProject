using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;
public class BaseController : ControllerBase
{
    SiteProvider? provider;
    protected SiteProvider Provider => provider ??= new SiteProvider(HttpContext.RequestServices.GetRequiredService<IConfiguration>());
}