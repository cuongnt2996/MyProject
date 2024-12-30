using System.Net;
using Microsoft.Extensions.Options;
using WebApi.Services;

namespace WebApi.Models;

public class VnPaymentService
{
    IHttpContextAccessor accessor;
    VnPaymentRequest request;

    public VnPaymentService(IHttpContextAccessor accessor, IOptions<VnPaymentRequest> options){
        this.accessor = accessor;
        request = options.Value;
    }

    public string? ToUrlPayment(Invoice obj){
        if(accessor.HttpContext !=null && accessor.HttpContext.Connection.RemoteIpAddress !=null)
        {
            SortedDictionary<string, string> dict = new SortedDictionary<string, string>{
                {"vnp_Version", request.Version},
                {"vnp_Command",request.Command},
                {"vnp_TmnCode",request.TmnCode},
                {"vnp_Amount", (obj.Amount*100).ToString("F0")},
                // {"vnp_BankCode", request.BankCode},
                {"vnp_CreateDate",DateTime.Now.ToString("yyyyMMddHHmmss")},
                {"vnp_CurrCode",request.CurrCode},
                {"vnp_IpAddr", "10.0.0.84"},
                {"vnp_Locale",request.Locale},
                {"vnp_OrderInfo", $"{obj.Email} payment with: {obj.Amount}"},
                {"vnp_OrderType",$"?"},
                {"vnp_ReturnUrl",request.ReturnUrl},
                {"vnp_ExpireDate", DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss")},
                {"vnp_TxnRef",obj.InvoiceId.ToString()}
             };
            string queryString = string.Join("&", dict.Select(p => $"{p.Key}={WebUtility.UrlEncode(p.Value)}"));           
            return request.SandboxUrl + "?" + queryString + "&vnp_SecureHash=" + Helper.HmaxSHA(request.HashSecret, queryString);
        }
        return null;
       
    }
}