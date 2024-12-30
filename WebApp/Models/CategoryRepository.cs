
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
namespace WebApp.Models;
public class CategoryRepository : BaseRepository
{
    IMemoryCache cache;
    public CategoryRepository(IConfiguration configuration, IMemoryCache cache) : base(configuration)
    {
        this.cache = cache;
    }   
    public async Task<IEnumerable<Category>?> GetCategories(){
        IEnumerable<Category>? list = cache.Get<IEnumerable<Category>>("categories");      
        if(list is null){
            // System.Console.WriteLine("lay tu db");
            using(HttpClient client = new HttpClient{BaseAddress =baseUri}){
                list =  await client.GetFromJsonAsync<IEnumerable<Category>>("/api/category");               
            }
            cache.Set("categories",list);
        }
        return list;
    }
    public async Task<IEnumerable<CategoryWithQuantity>?> GetCategoriesByProduct(string keyword){    
            using(HttpClient client = new HttpClient{BaseAddress =baseUri}){
                return await client.GetFromJsonAsync<IEnumerable<CategoryWithQuantity>>($"/api/category/keyword?keyword={keyword}");               
            }
    }
    public async Task<Category?> GetCategoryAsync(int id){
        var Categories = await GetCategories();
        Category? category = Categories?.FirstOrDefault(c=>c.CategoryId == id);
        return category;
    }
    public async Task<int> Add(Category obj)
    {
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PostAsJsonAsync<Category>("/api/category",obj);
            if(message.IsSuccessStatusCode){
                cache.Remove("categories");
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
    public async Task<int> CheckDelete(int id){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            return await client.GetFromJsonAsync<int>($"/api/category/{id}");
        }
    }
        public async Task<int> Edit(Category obj){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PutAsJsonAsync<Category>($"/api/Category/{obj.CategoryId}",obj);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
}