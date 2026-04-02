namespace GroceryStore.Shared.Interfaces.Repositories;

public interface IEndpoint
{
    static abstract void MapEndpoint(IEndpointRouteBuilder app);
}