using System.Data;
using Dapper;

namespace WebApi.Models;
public class ProductRepository : BaseRepository
{
    public ProductRepository(IDbConnection connection) : base(connection)
    {
    }
    // public IEnumerable<Product> GetProducts(){
    //     string sql = "SELECT P.*,C.CategoryName FROM Product P WITH(NOLOCK) INNER JOIN Category C WITH(NOLOCK) ON C.CategoryID = P.CategoryId WHERE P.IsDeleted = 0";
    //     return connection.Query<Product>(sql);
    // }
    public Product? GetProduct(int id){
        string sql = "SELECT * FROM Product P WITH(NOLOCK) WHERE ProductId = @Id";
        return connection.QueryFirstOrDefault<Product>(sql, new{Id = id});
    }
    public int Edit(Product obj){
        string sql = "UPDATE Product SET CategoryId = @CategoryId, ProductName = @ProductName,  Description = @Description, Unit = @Unit, ImageUrl = @ImageUrl, IsActived = @IsActived, Price =@Price WHERE ProductID = @ProductId";
        return connection.Execute(sql,obj);
    }
    public int Add(Product obj){
        string sql = "INSERT INTO Product(ProductName, CategoryId, Description, Unit, ImageUrl, CreatedUserId, Price) VALUES (@ProductName, @CategoryId, @Description, @Unit, @ImageUrl, @CreatedUserId, @Price)";
        return connection.Execute(sql,obj);
    }
    public string? GetProductImageUrl(int id){
        return connection.ExecuteScalar<string>("SELECT ImageUrl FROM Product WHERE ProductId = @Id",new{Id = id});
    }
    public int Delete(int id, int userId){
        string sql = "UPDATE Product SET IsDeleted = 1, DeletedUserId = @UserId, DeletedDate = GETDATE() WHERE ProductId = @Id";
        return connection.Execute(sql,new{Id = id, UserId = userId});
    }
    public IEnumerable<Product> GetProductsAsync(int page, int size, int categoryId, string keyword){
        var parameters = new DynamicParameters(); 
            parameters.Add("@Type", "LoadList"); 
            parameters.Add("@CategoryId", categoryId); 
            parameters.Add("@Page", page); 
            parameters.Add("@Size", size); 
            parameters.Add("@KeyWord", keyword); 
        return connection.Query<Product>("sp_Product",parameters, commandType:CommandType.StoredProcedure);
    }
    public int CountProduct(int page, int size, int categoryId, string keyword){
        var parameters = new DynamicParameters(); 
            parameters.Add("@Type", "CountProduct"); 
            parameters.Add("@CategoryId", categoryId); 
            parameters.Add("@Page", page); 
            parameters.Add("@Size", size); 
            parameters.Add("@KeyWord", keyword); 
        return connection.ExecuteScalar<int>("sp_Product",parameters, commandType:CommandType.StoredProcedure);
    }
}