using System.Data;
using System.Security.Cryptography.X509Certificates;
using Dapper;

namespace WebApi.Models;
public class InvoiceRepository : BaseRepository
{
    public InvoiceRepository(IDbConnection connection) : base(connection)
    {
    }
    public IEnumerable<Invoice> GetInvoices(int userId){
        return connection.Query<Invoice>("SELECT * FROM Invoice WHERE [CreatedUserId] = @userId ORDER BY CreatedDate DESC" ,new{userId});
    }
    public Invoice GetInvoice(string id){
       using(var rs = connection.QueryMultiple("Invoices", new{Type = "GetInvoice",InvoiceId =id}))
       {
            var invoice = rs.ReadSingle<Invoice>();
            var invoiceItem = rs.Read<InvoiceItem>();
            invoice.InvoiceItems = invoiceItem.ToList();
            return invoice;
        }      
    }
    public int Add(Invoice obj){
        var invoiceItems = new DataTable(); 
        invoiceItems.Columns.Add("InvoiceId", typeof(string)); 
        invoiceItems.Columns.Add("CartId", typeof(int)); 
        invoiceItems.Columns.Add("ProductId", typeof(int)); 
        invoiceItems.Columns.Add("ProductName", typeof(string)); 
        invoiceItems.Columns.Add("ImageUrl", typeof(string)); 
        invoiceItems.Columns.Add("Price", typeof(decimal)); 
        invoiceItems.Columns.Add("Quantity", typeof(int));
        foreach (var item in obj.InvoiceItems) 
        { 
            invoiceItems.Rows.Add(item.InvoiceId, item.CartId, item.ProductId, item.ProductName, item.ImageUrl, item.Price, item.Quantity); 
        } 
        var parameters = new DynamicParameters(); 
        parameters.Add("@InvoiceId", obj.InvoiceId); 
        parameters.Add("@InvoiceCode",obj.InvoiceCode);
        parameters.Add("@CreatedUserId", obj.CreatedUserId); 
        parameters.Add("@CreatedDate", obj.CreatedDate); 
        parameters.Add("@CustomerName", obj.CustomerName); 
        parameters.Add("@Email", obj.Email); 
        parameters.Add("@Address", obj.Address); 
        parameters.Add("@Phone", obj.Phone); 
        parameters.Add("@Amount", obj.Amount); 
        parameters.Add("@InvoiceItems", 
        invoiceItems.AsTableValuedParameter("dbo.InvoiceItemType")); 
        parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output); 
        
        connection.Execute("InsertInvoiceAndItems", parameters, commandType: CommandType.StoredProcedure); 
        return parameters.Get<int>("@Result");
    }
    public int Paid(string invoiceId){
        return connection.Execute("UPDATE Invoice SET IsPaid = 1 WHERE InvoiceId = @invoiceId",new{invoiceId});
    }
    public string? GenarateInvoiceCode(){
        return connection.ExecuteScalar<string>("GenarateInvoiceCode",commandType:CommandType.StoredProcedure);
    }
}