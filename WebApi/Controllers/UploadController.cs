using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UploadController :BaseController
{
    [HttpPost]
    public string? Upload(IFormFile f){
        return Helper.Upload(f);
    }
}