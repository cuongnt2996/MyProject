using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models;
// [Table("VnPayment")]
public class VnPaymentResponse
{
    [Key]
    public int VnPaymentId {get; set;}
    [BindProperty(Name = "vnp_Amount")]
    public long Amount { get; set; }
    [BindProperty(Name = "vnp_BankCode")]
    public string BankCode { get; set; } = null!;
    [BindProperty(Name = "vnp_BankTranNo")]
    public string? BankTranNo { get; set; } 
    [BindProperty(Name = "vnp_CardType")]
    public string CardType { get; set; } = null!;
    [BindProperty(Name = "vnp_OrderInfo")]
    public string OrderInfo { get; set; } = null!;
    [BindProperty(Name = "vnp_PayDate")]
    public string PayDate { get; set; } = null!;
    [BindProperty(Name = "vnp_ResponseCode")]
    public string? ResponseCode { get; set; }
    [BindProperty(Name = "vnp_TmnCode")]
    public string TmnCode { get; set; } = null!;
    [BindProperty(Name = "vnp_TransactionNo")]
    public string TransactionNo { get; set; } = null!;
    [BindProperty(Name = "vnp_TransactionStatus")]
    public string TransactionStatus { get; set; } = null!;
    [BindProperty(Name = "vnp_TxnRef")]
    public string TxnRef { get; set; } = null!;
    [BindProperty(Name = "vnp_SecureHash")]
    public string SecureHash { get; set; } = null!;

}