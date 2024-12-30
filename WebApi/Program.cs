using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
string secretKey = builder.Configuration.GetValue<string>("Jwt:SecretKey") ?? "";
builder.Services.AddAuthentication(p => {
    p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(p => {
    p.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidIssuer = "cse.net.vn",
        ValidAudience = "cse.net.vn",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<VnPaymentService>();
builder.Services.Configure<VnPaymentRequest>(builder.Configuration.GetSection("Payments:VnPayment"));
var app = builder.Build();
app.UseStaticFiles();
app.MapControllers();
app.Run();
