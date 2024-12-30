using Newtonsoft.Json;

namespace WebApp.Models;
public class InvoiceRepository : BaseRepository
{
    public InvoiceRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<string?> Add(Invoice obj){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            // System.Console.WriteLine("Sending request to: " + client.BaseAddress + "api/invoice"); 
            // System.Console.WriteLine("Invoice Data: " + JsonConvert.SerializeObject(obj));
            HttpResponseMessage message = await client.PostAsJsonAsync<Invoice>("/api/invoice",obj);
            System.Console.WriteLine(message.IsSuccessStatusCode);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
    public async Task<int> Paid(string invoiceId){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            HttpResponseMessage message = await client.PutAsJsonAsync($"/api/invoice",invoiceId);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
    public async Task<int> PaymentResponse(VnPaymentResponse obj){
        using(HttpClient client = new HttpClient{BaseAddress=baseUri}){
            HttpResponseMessage message = await client.PostAsJsonAsync<VnPaymentResponse>($"/api/invoice/paymentresponse",obj);
            if(message.IsSuccessStatusCode){
                return await message.Content.ReadFromJsonAsync<int>();
            }
            return -1;
        }
    }
    public async Task<IEnumerable<Invoice>?> GetInvoices(int userId){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            return await client.GetFromJsonAsync<IEnumerable<Invoice>>($"/api/invoice?userId={userId}");
        }
    }
    public async Task<Invoice?> GetInvoice(string invoiceId){
        using(HttpClient client = new HttpClient{BaseAddress = baseUri}){
            System.Console.WriteLine("Sending request to: " + client.BaseAddress + $"/api/invoice/detail?invoiceId={invoiceId}");
            return await client.GetFromJsonAsync<Invoice>($"/api/invoice/detail?invoiceId={invoiceId}");
        }
    }
    
}