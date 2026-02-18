using System.Data;
using Npgsql;

namespace GroceryStore.Infrastructure;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => new NpgsqlConnection(connectionString);
}