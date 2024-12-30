using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApp.Models;

namespace WebApp.Controllers;
public class BaseController :Controller
{
    SiteProvider? provider;
    protected SiteProvider Provider => provider ??= new SiteProvider(HttpContext.RequestServices.GetRequiredService<IConfiguration>(), HttpContext.RequestServices.GetRequiredService<IMemoryCache>());
}