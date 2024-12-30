using System.Data;
using Dapper;
using WebApi.Models;

namespace WebApi.Models;

public class VnPaymentRepository:BaseRepository
{
    public VnPaymentRepository(IDbConnection connection) : base(connection)
    {
    }

    public int Add(VnPaymentResponse obj){
        string sql= "INSERT INTO VnPayment(Amount, BankCode, BankTranNo, CardType, OrderInfo, PayDate, ResponseCode, TmnCode, TransactionNo, TransactionStatus, TxnRef, SecureHash) VALUES (@Amount, @BankCode, @BankTranNo, @CardType, @OrderInfo, @PayDate, @ResponseCode, @TmnCode, @TransactionNo, @TransactionStatus, @TxnRef, @SecureHash)";
        return connection.Execute(sql,obj);
    }
}