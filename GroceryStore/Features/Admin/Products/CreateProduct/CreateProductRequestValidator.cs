namespace GroceryStore.Features.Admin.Products.CreateProduct;

using FluentValidation;
using Shared.Consts.ValidationMessages;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ProductValidationMessages.NameRequired)
            .MaximumLength(50).WithMessage("Maximum length of Name is 50");

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0).WithMessage(ProductValidationMessages.PriceNegative);

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .GreaterThan(0).WithMessage("CategoryId must be greater than 0");

        RuleFor(p => p.BrandId).GreaterThan(0);
        RuleFor(p => p.CountryId).GreaterThan(0);

        RuleFor(p => p.Attributes)
            .NotEmpty().WithMessage(ProductValidationMessages.AttributesRequired)
            .NotNull().WithMessage(ProductValidationMessages.AttributesRequired);

        RuleForEach(p => p.Attributes)
            .ChildRules(a =>
            {
                a.RuleFor(x => x.Id).GreaterThan(0);
                a.RuleFor(x => x.Value).NotEmpty();
            });

        RuleFor(p => p.Attributes)
            .Must(a => a.Select(x => x.Id).Distinct().Count() == a.Count)
            .WithMessage(ProductValidationMessages.AttributeDuplicates);
    }
}