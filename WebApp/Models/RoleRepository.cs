
namespace WebApp.Models;
public class RoleRepository : BaseRepository
{
    public RoleRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<IEnumerable<Role>?> GetRolesAsync()
    {
        using (HttpClient client = new HttpClient { BaseAddress = baseUri })
        {
            return await client.GetFromJsonAsync<IEnumerable<Role>>("/api/role");
        }
    }
    public async Task<Role?> GetRoleAsync(int id)
    {
        using (HttpClient client = new HttpClient { BaseAddress = baseUri })
        {
            return await client.GetFromJsonAsync<Role>($"/api/role/{id}");
        }
    }
    public async Task<int> Add(Role obj)
    {
        using (HttpClient client = new HttpClient { BaseAddress = baseUri })
        {
            HttpResponseMessage message = await client.PostAsJsonAsync<Role>("/api/role", obj);
            if (message.IsSuccessStatusCode)
            {
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
    public async Task<int> EditAsync(Role obj)
    {
        using (HttpClient client = new HttpClient { BaseAddress = baseUri })
        {
            HttpResponseMessage message = await client.PutAsJsonAsync<Role>("/api/role", obj);
            if (message.IsSuccessStatusCode)
            {
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
    public async Task<int> DeleteAsync(Role obj)
    {
        using (HttpClient client = new HttpClient { BaseAddress = baseUri })
        {
            try { HttpResponseMessage message = await client.PutAsJsonAsync("/api/role/delete", obj); if (message.IsSuccessStatusCode) { return await message.Content.ReadFromJsonAsync<int>(); } else { string errorContent = await message.Content.ReadAsStringAsync(); Console.WriteLine($"Error: {message.StatusCode}, Content: {errorContent}"); } } catch (Exception ex) { Console.WriteLine($"Exception: {ex.Message}"); }
            return -1;
        }
    }
}