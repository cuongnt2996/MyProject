
// using WebApi.Models;

using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;
public class ProductRepository : BaseRepository
{
    
    public ProductRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<ProductPage?> GetProducts(int page,int size,int categoryId,string keyword){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            return await client.GetFromJsonAsync<ProductPage>($"/api/product/{page}/{size}/{categoryId}/{keyword}");
        }
    }
    public async Task<int> Add(Product obj){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PostAsJsonAsync<Product>("/api/product",obj);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
    public async Task<Product?> GetProductAsync(int id){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            return await client.GetFromJsonAsync<Product>($"/api/product/{id}");
        }
    }
    public async Task<int> Edit(Product obj){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PutAsJsonAsync<Product>($"/api/product/{obj.ProductId}",obj);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
    public async Task<int> Delete(int id, int userId){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.DeleteAsync($"/api/product/{id}?userId={userId}");
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }

}