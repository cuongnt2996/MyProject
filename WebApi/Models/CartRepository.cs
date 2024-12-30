using System.Data;
using Dapper;

namespace WebApi.Models;
public class CartRepository : BaseRepository
{
    public CartRepository(IDbConnection connection) : base(connection)
    {
    }
    public IEnumerable<Cart> GetCarts(int userId, string clientId){
        return connection.Query<Cart>("GetCarts", new{UserId = userId, ClientId = clientId}, commandType: CommandType.StoredProcedure);
    }
    public int Add(Cart obj){
        // string sql = "INSERT INTO Cart(ProductId, Quantity, UserId, ClientId) VALUES (@ProductId, @Quantity, @UserId, @ClientId)";
        return connection.Execute("AddToCart",new{obj.UserId, obj.ProductId, obj.Quantity, obj.ClientId},commandType: CommandType.StoredProcedure);
    }
}
// public int Add(Product obj)
// {
//     string sql = @"
//     INSERT INTO Product(ProductName, CategoryId, Description, Unit, ImageUrl, CreatedUserId, Price) 
//     VALUES (@ProductName, @CategoryId, @Description, @Unit, @ImageUrl, @CreatedUserId, @Price);
//     SELECT CAST(SCOPE_IDENTITY() as int)";
//     return connection.QuerySingle<int>(sql, obj);
// }
