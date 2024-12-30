using System.Data;

namespace WebApi.Models;
public abstract class BaseRepository
{
    protected IDbConnection connection;
    public BaseRepository(IDbConnection connection)
    {
        this.connection = connection;
    }
}