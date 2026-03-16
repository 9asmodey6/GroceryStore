namespace GroceryStore.Features.Admin.Brands.AddBrand;

using Database.Entities.Brand;
using FluentValidation;
using GetBrands;

public class AddBrandValidator : AbstractValidator<AddBrandRequest>, IValidator<AddBrandRequest>
{
    public AddBrandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters")
            .MinimumLength(3).WithMessage("Name cannot exceed 3 characters");
    }
}