namespace GroceryStore.Application.Abstractions;

public interface IFeature
{
    static abstract void ConfigureServices(IServiceCollection services);
}