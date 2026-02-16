namespace GroceryStore.Infrastructure;
using Npgsql;
using System.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

