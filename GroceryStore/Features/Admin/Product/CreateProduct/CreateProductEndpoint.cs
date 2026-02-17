namespace GroceryStore.Features.Admin.Product.CreateProduct;

using Infrastructure;

public class CreateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/admin/products/{ProductRequestDto}", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Creates a new Product")
            .WithName("CreateProduct")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }
    
    private static async Task<IResult> HandleAsync()
    {
        
    }
}