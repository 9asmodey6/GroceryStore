namespace GroceryStore.Tests.Infrastructure.Features.CreateProductFeature;

using FluentAssertions;
using GroceryStore.Features.Admin.Products.CreateProduct;
using Shared.Consts.ValidationMessages;
using Shared.Models;

public class CreateProductRequestValidatorTests
{
    private readonly CreateProductRequestValidator _validator;

    public CreateProductRequestValidatorTests()
    {
        _validator = new CreateProductRequestValidator();
    }

    [Fact]
    public void Validate_CreateProductRequest_IsValid()
    {
        var product = new CreateProductRequest(
            Name: "TestProduct",
            CategoryId: 1,
            BrandId: 1,
            CountryId: 1,
            Description: "Test Product Description",
            Price: 1000,
            BaseUnit: "Tests",
            new List<EnumerationModel>
            {
                new(1, "TestProductDescription"),
                new(2, "TestProductPrice")
            });

        var result = _validator.Validate(product);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_CreateProductRequest_ShouldReturn_NameIsRequired()
    {
        var product = new CreateProductRequest(
            Name: null!,
            CategoryId: 1,
            BrandId: 1,
            CountryId: 1,
            Description: "Test Product Description",
            Price: 1000,
            BaseUnit: "Tests",
            new List<EnumerationModel>
            {
                new(1, "TestProductDescription"),
                new(2, "TestProductPrice")
            });

        var result = _validator.Validate(product);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorMessage.Should().Be(ProductValidationMessages.NameRequired);
    }

    [Fact]
    public void Validate_CreateProductRequest_ShouldReturn_PriceCantBeNegative()
    {
        var product = new CreateProductRequest(
            Name: "Test",
            CategoryId: 1,
            BrandId: 1,
            CountryId: 1,
            Description: "Test Product Description",
            Price: -100,
            BaseUnit: "Tests",
            new List<EnumerationModel>
            {
                new(1, "TestProductDescription"),
                new(2, "TestProductPrice")
            });

        var result = _validator.Validate(product);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorMessage.Should().Be(ProductValidationMessages.PriceNegative);
    }

    [Fact]
    public void Validate_CreateProductRequest_ShouldReturn_AttrebutesAreRequired()
    {
        var product = new CreateProductRequest(
            Name: "Test",
            CategoryId: 1,
            BrandId: 1,
            CountryId: 1,
            Description: "Test Product Description",
            Price: 100,
            BaseUnit: "Tests",
            new List<EnumerationModel>());

    var result = _validator.Validate(product);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorMessage.Should().Be(ProductValidationMessages.AttributesRequired);
    }
    
    [Fact]
    public void Validate_CreateProductRequest_ShouldReturn_AttributesDuplicates()
    {
        var product = new CreateProductRequest(
            Name: "Test",
            CategoryId: 1,
            BrandId: 1,
            CountryId: 1,
            Description: "Test Product Description",
            Price: 100,
            BaseUnit: "Tests",
            new List<EnumerationModel>
            {
                new(1, "TestProductAttribute"),
                new(1, "TestProductAttribute")
            });

        var result = _validator.Validate(product);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorMessage.Should().Be(ProductValidationMessages.AttributeDuplicates);
    }
}