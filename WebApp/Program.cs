using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(p => {
    p.LoginPath = "/auth/login";
    p.LogoutPath = "/auth/logout";
    p.AccessDeniedPath = "/auth/denined";
    p.Cookie.Name = "cebnetvn";
    p.ExpireTimeSpan = TimeSpan.FromDays(30);
});
builder.Services.AddMemoryCache();
// builder.Services.AddScoped<IMemoryCache, MemoryCache>();
builder.Services.AddMvc();

var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name:"dashboard",pattern:"{area:exists}/{controller=home}/{action=index}/{id?}");
app.MapDefaultControllerRoute();
app.Run();
