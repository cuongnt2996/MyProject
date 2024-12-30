
namespace WebApp.Models;
public class UploadRepository : BaseRepository
{
    public UploadRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<string?> Upload(IFormFile f){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri})
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StreamContent(f.OpenReadStream()),"f",f.FileName);
            HttpResponseMessage message = await client.PostAsync("/api/upload",content);
            if(message.IsSuccessStatusCode){
                
                return await message.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}