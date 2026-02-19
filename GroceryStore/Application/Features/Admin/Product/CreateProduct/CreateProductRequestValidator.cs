namespace GroceryStore.Application.Features.Admin.Product.CreateProduct;

using FluentValidation;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Maximum length of Name is 50");

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .GreaterThan(0).WithMessage("CategoryId must be greater than 0");

        RuleFor(p => p.Attributes)
            .NotEmpty().WithMessage("Attributes are required")
            .NotNull().WithMessage("Attributes are required");

        RuleForEach(p => p.Attributes)
            .ChildRules(a =>
            {
                a.RuleFor(x => x.AttributeId).GreaterThan(0);
                a.RuleFor(x => x.Value).NotEmpty();
            });

        RuleFor(p => p.Attributes)
            .Must(a => a.Select(x => x.AttributeId).Distinct().Count() == a.Count)
            .WithMessage("Duplicate attributeId in request");
    }
}