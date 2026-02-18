namespace GroceryStore.Infrastructure.Interfaces;

using System.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

