using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Dapper;

namespace WebApi.Models;
public class RoleRepository : BaseRepository
{
    public RoleRepository(IDbConnection connection) : base(connection)
    {
    }
    public int Add(Role obj){
        string sql = "INSERT INTO Role (RoleCode, RoleName, CreatedUserId) VALUES (@RoleCode, @RoleName, @CreatedUserId)";
        return connection.Execute(sql,obj);
    }
    public IEnumerable<Role> GetRoles(){
        string sql = "SELECT * FROM Role R WITH(NOLOCK) WHERE IsDeleted = 0 ";
        return connection.Query<Role>(sql);
    }
    public Role? GetRole(int id){
        string sql = "SELECT * FROM Role R WITH(NOLOCK) WHERE R.RoleId = @id ";
        return connection.QueryFirstOrDefault<Role>(sql,new{id});
    }
    public int Edit(Role obj){
        string sql= "UPDATE Role SET RoleCode =  @RoleCode, RoleName = @RoleName, IsActived = @IsACtived WHERE RoleID = @RoleId";
        return connection.Execute(sql,obj);
    }
    public int Delete(Role obj){
        string sql="UPDATE Role SET IsDeleted = 1, DeletedUserId = @DeletedUserId, DeletedDate = GETDATE() WHERE RoleId = @RoleId";
        return connection.Execute(sql,obj);
    }

}