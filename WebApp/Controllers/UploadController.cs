using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
public class UploadController:BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add(IFormFile f){

        if(f!=null){

            return Json(await Provider.Upload.Upload(f));
        }
        return Json(null);
    }
}