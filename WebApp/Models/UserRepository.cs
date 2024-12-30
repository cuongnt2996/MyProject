
using System.Net.Http.Headers;

namespace WebApp.Models;
public class UserRepository : BaseRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<User?> GetUserAsync(string identifier){
        using(HttpClient client = new HttpClient{BaseAddress =baseUri}){
            return await client.GetFromJsonAsync<User>($"/api/User/{identifier}");
        }
    }
    // public async Task<User?> GetUserByIdAsync(int id){
    //     using(HttpClient client = new HttpClient{BaseAddress =baseUri}){
    //         return await client.GetFromJsonAsync<User>($"/api/User/{id}");
    //     }
    // }
    public async Task<IEnumerable<User>?> GetUsersAsync(string token){
        using(HttpClient client = new HttpClient{BaseAddress =baseUri}){
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await client.GetFromJsonAsync<IEnumerable<User>>("/api/user/all");
        }
    }
    // public async Task<User?> GetUserAsync(string userName,string password){
    //     using(HttpClient client = new HttpClient{BaseAddress =baseUri}){
    //         return await client.GetFromJsonAsync<User>($"User/{userName}/{password}");
    //     }
    // }
    public async Task<int?> CheckExistUser(string userName, string email){
        using(HttpClient client = new HttpClient{BaseAddress =baseUri}){
            HttpResponseMessage message = await client.GetAsync($"/api/User/{userName}/{email}");
            if(message.IsSuccessStatusCode){
                var rs = await message.Content.ReadFromJsonAsync<int>();
                return rs;
            }
            return -1;
        }
    }
    public async Task<int?> Add(User obj){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PostAsJsonAsync("/api/user",obj);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
    public async Task<string?> LoginAsync(LoginModel obj){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PostAsJsonAsync("/api/user/login",obj);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
    public async Task<int> Edit(User obj){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PutAsJsonAsync<User>("/api/user",obj);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
}