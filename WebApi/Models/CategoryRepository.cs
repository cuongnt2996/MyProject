using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Dapper;

namespace WebApi.Models;
public class CategoryRepository : BaseRepository
{
    public CategoryRepository(IDbConnection connection) : base(connection)
    {
    }
    public IEnumerable<Category> GetCategories(){
        string sql ="SELECT * FROM Category C WITH(NOLOCK) WHERE C.IsDeleted = 0";
        return connection.Query<Category>(sql);
    }
    // public Category GetCategory(int id){
    //     string sql = "SELECT * FROM Category C WITH(NOLOCK) WHERE C.IsDeleted = 0 AND CategoryId = @Id";
    //     return connection.QueryFirst<Category>(sql,new {Id=id});
    // }
    public int Add(Category obj){
        string sql = "INSERT INTO Category(CategoryName, CreatedUserId) VALUES (@CategoryName, @CreatedUserId)";
        return connection.Execute(sql,obj);
    }
    public int Edit(Category obj){
        string sql = "UPDATE Category SET CategoryName = @CategoryName,IsActived = @IsActived WHERE CategoryId = @CategoryId";
        return connection.Execute(sql,obj);
    }
    public int Delete(int id, int userId){
        string sql = "UPDATE Category SET IsDeleted = 1, DeletedUserId = @UserId, DeletedDate = GETDATE() WHERE Category = @Id";
        return connection.Execute(sql,new{Id = id, UserId = userId});
    }
    public int CheckDelete(int id){
        string sql = "SELECT COUNT(*) FROM Product P WITH(NOLOCK) WHERE CategoryId = @Id";
        return connection.ExecuteScalar<int>(sql,new {Id = id});
    }
    public IEnumerable<CategoryWithQuantity> GetCategoriesByProduct(string keyword){
        return connection.Query<CategoryWithQuantity>("Sp_CategoryWithQuantity", new {keyword});
    }

}