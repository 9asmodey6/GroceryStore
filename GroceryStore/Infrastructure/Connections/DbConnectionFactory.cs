namespace GroceryStore.Infrastructure.Connections;

using System.Data;
using Interfaces;
using Npgsql;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => new NpgsqlConnection(connectionString);
}