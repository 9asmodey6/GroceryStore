namespace GroceryStore.Database;

using System.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

