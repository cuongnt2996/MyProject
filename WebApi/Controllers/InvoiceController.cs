using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class InvoiceController:BaseController
{
    // public Invoice GetInvoice(string id){
    //     return Provider.Invoice.GetInvoice(id);
    // }
    VnPaymentService service;
    public InvoiceController(VnPaymentService service)
    {
        this.service = service;
    }
    [HttpGet]
    public IEnumerable<Invoice> GetInvoices(int userId){
        return Provider.Invoice.GetInvoices(userId);
    }
    [HttpGet("detail")]
    public Invoice GetInvoice(string invoiceId){
        return Provider.Invoice.GetInvoice(invoiceId);
    }
    [HttpPost]
    public string? Add(Invoice obj){
        if(string.IsNullOrEmpty(obj.InvoiceCode)){
            obj.InvoiceCode = Provider.Invoice.GenarateInvoiceCode();
        }
        var rs = Provider.Invoice.Add(obj);
        if(rs>0){           
            return service.ToUrlPayment(obj);
        }
        return null;
    }
    [HttpPut]
    public int Paid(string invoiceId){
        return Provider.Invoice.Paid(invoiceId);
    }
    [HttpPost("PaymentResponse")]
    public int PaymentResponse(VnPaymentResponse obj){
        return Provider.VnPayment.Add(obj);
    }
}