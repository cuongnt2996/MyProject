
namespace WebApp.Models;
public class CartRepository : BaseRepository
{
    public CartRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<IEnumerable<Cart>?> GetCarts(int userId, string clienId){
        using(HttpClient client = new HttpClient{BaseAddress =baseUri}){
            return await client.GetFromJsonAsync<IEnumerable<Cart>>($"/api/cart?userId={userId}&clientId={clienId}");
        }
    }
    public async Task<int> AddAsync(Cart obj){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PostAsJsonAsync<Cart>("/api/cart",obj);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
}