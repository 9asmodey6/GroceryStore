namespace GroceryStore.Features.Admin.Products.UpdateProduct;

using FluentValidation;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.NewName.Value)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Maximum length of Name is 50")
            .When(p => p.NewName.HasValue);

        RuleFor(p => p.NewPrice.Value)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0")
            .When(p => p.NewPrice.HasValue);

        RuleFor(p => p.NewCategoryId.Value)
            .NotEmpty().WithMessage("CategoryId is required")
            .GreaterThan(0).WithMessage("CategoryId must be greater than 0")
            .When(p => p.NewCategoryId.HasValue);
    }
}