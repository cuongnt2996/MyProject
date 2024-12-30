namespace WebApp.Models;
public abstract class BaseRepository
{
    public Uri baseUri;
    public BaseRepository(IConfiguration configuration){
        baseUri = new Uri(configuration["ApiUrl"] ?? throw new Exception("Not found ApiUri"));
    }
}