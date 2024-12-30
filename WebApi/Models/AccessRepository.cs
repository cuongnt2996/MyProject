using System.Data;
using Dapper;

namespace WebApi.Models;
public class AccessRepository : BaseRepository
{
    public AccessRepository(IDbConnection connection) : base(connection)
    {
    }
    public int Add(Access obj){
        return connection.Execute("INSERT INTO Access(AccessName, AccessUrl, ParentId) VALUES (@AccessName,@AccessUrl,@ParentId)",obj);
    }
    public IEnumerable<Access> GetAccesses(){
        return connection.Query<Access>("SELECT * FROM Access");
    }
    public IEnumerable<Access> GetParent(){
        return connection.Query<Access>("SELECT * FROM Access WHERE ParentId IS NULL");
    }
    public IEnumerable<Access> GetAccessesByUser(string id){
        return connection.Query<Access>("GetAccessesByUserId",new{id},commandType:CommandType.StoredProcedure);
    }
    public IEnumerable<AccessChecked> GetAccessesChecked(string userId){
        return connection.Query<AccessChecked>("GetAccessesByUser",new {UserId = userId}, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<AccessChecked> GetAccessesCheckedsByRole(int id){
        return connection.Query<AccessChecked>("GetAccessCheckedsRole",new {Roleid = id}, commandType: CommandType.StoredProcedure);
    }
}