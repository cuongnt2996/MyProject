using System.Data;
using System.Reflection.Metadata.Ecma335;
using Dapper;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Models;
public class UserRepository : BaseRepository
{
    public UserRepository(IDbConnection connection) : base(connection)
    {
    }
    public int Add(User obj){
        string sql = "INSERT INTO [User] (UserName, Email, Password, PasswordSalt, FirstName, LastName, DateOfBirth, RoleId) VALUES (@UserName, @Email, @Password, @PasswordSalt, @FirstName, @LastName, @DateOfBirth, @RoleId)";
        return connection.Execute(sql,obj);
    }
    public IEnumerable<User> GetUsers(){
        string sql = "SELECT U.*, RoleCode, RoleName FROM [User] U WITH(NOLOCK) LEFT JOIN [Role] R WITH(NOLOCK) ON R.RoleId = U.RoleId";
        return connection.Query<User>(sql);
    }
    public User? GetUser(string userName){
        string sql = "SELECT U.*, RoleCode, RoleName FROM [User] U WITH(NOLOCK) LEFT JOIN [Role] R WITH(NOLOCK) ON R.RoleId = U.RoleId WHERE [Email] = @UserName OR [UserName] = @UserName";
        return connection.QueryFirstOrDefault<User>(sql, new {UserName = userName});
    }
    public User? GetUserById(int id){
        string sql = "SELECT U.*, RoleCode, RoleName FROM [User] U WITH(NOLOCK) LEFT JOIN [Role] R WITH(NOLOCK) ON R.RoleId = U.RoleId WHERE [UserID] = @UserID";
        return connection.QueryFirstOrDefault<User>(sql, new {UserID = id});
    }
    // public User? GetUser(string userName, string password){
    //     string sql = "SELECT U.*, RoleCode, RoleName FROM [User] U WITH(NOLOCK) LEFT JOIN [Role] R WITH(NOLOCK) ON R.RoleId = U.RoleId WHERE [Email] = Password = @Password OR [UserName] = @UserName)";
    //     return connection.QueryFirstOrDefault<User>(sql, new {UserName = userName, Password = password});
    // }
    public int? CheckExistsUser(string userName, string email){
        string sql = "SELECT COUNT(*) FROM [User] U WITH(NOLOCK) WHERE [UserName] = @UserName OR [Email] = @Email";
        return connection.ExecuteScalar<int>(sql,new{Email =email, UserName = userName});
    }
    public User? Login(LoginModel obj){
        string sql = "SELECT U.*, RoleCode, RoleName FROM [User] U WITH(NOLOCK) LEFT JOIN [Role] R WITH(NOLOCK) ON R.RoleId = U.RoleId WHERE ([Email] = @UserName OR [UserName] = @UserName) AND Password = @Password";
        return connection.QueryFirstOrDefault<User>(sql,obj);
    }
    public int Edit(User obj){
        string sql = "UPDATE [User] SET RoleId = @RoleId, FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth WHERE [UserID] = @UserID";
        return connection.Execute(sql,obj);
    }

}