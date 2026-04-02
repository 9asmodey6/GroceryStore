namespace GroceryStore.Tests.Infrastructure.Services;

using Database.Enums;
using FluentAssertions;
using GroceryStore.Infrastructure.Repositories.Categories;
using GroceryStore.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Shared.Enums;
using Shared.Extensions;
using Shared.Interfaces.Repositories;
using Shared.Models;

public class CategoryAttributeValueNormalizerTests
{
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly Mock<ICategoryAttributeRepository> _repositoryMock;
    private readonly CategoryAttributeValueNormalizer _sut;

    public CategoryAttributeValueNormalizerTests()
    {
        _cacheMock = new Mock<IMemoryCache>();
        _repositoryMock = new Mock<ICategoryAttributeRepository>();
        _sut = new CategoryAttributeValueNormalizer(_cacheMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public async Task ValidateAndNormalizeAsync_IntegerAttribute_ShouldNormalize()
    {
        var metadata = new List<MetadataAttribute>
        {
            new(
                AttributeId: 1,
                Name: "Weight",
                DataType: AttributeDataType.Integer,
                Unit: "g",
                MinValue: 0,
                MaxValue: 10000,
                IsRequired: true
            )
        };

        SetupCache(metadata, categoryId: 1);

        var values = new List<EnumerationModel>
        {
            new(1, "500")
        };

        var result = await _sut.ValidateAndNormalizeAsync(1, values, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().ContainKey(1);
        result.Value![1].Should().Be("500");
    }

    [Fact]
    public async Task ValidateAndNormalizeAsync_DecimalAttribute_ShouldRoundToTwoDecimals()
    {
        var metadata = new List<MetadataAttribute>
        {
            new(
                AttributeId: 1,
                Name: "FatContent",
                DataType: AttributeDataType.Decimal,
                Unit: "%",
                MinValue: 0,
                MaxValue: 100,
                IsRequired: true
            )
        };

        SetupCache(metadata, categoryId: 1);

        var values = new List<EnumerationModel>
        {
            new(1, "30.12345")
        };

        var result = await _sut.ValidateAndNormalizeAsync(1, values, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value![1].Should().Be("30.12");
    }

    [Fact]
    public async Task ValidateAndNormalizeAsync_OutOfRange_ShouldReturnError()
    {
        var metadata = new List<MetadataAttribute>
        {
            new(
                AttributeId: 1,
                Name: "Weight",
                DataType: AttributeDataType.Integer,
                Unit: "g",
                MinValue: 0,
                MaxValue: 10000,
                IsRequired: true)
        };

        SetupCache(metadata, categoryId: 1);

        var values = new List<EnumerationModel>
        {
            new(1, "99999")
        };

        var result = await _sut.ValidateAndNormalizeAsync(1, values, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Validation.Errors.Should().Contain(e =>
            e.Code == MetadataValidationErrorCode.OutOfRange.ToApiCode());
    }

    [Fact]
    public async Task ValidateAndNormalizeAsync_MissingRequiredAttribute_ShouldReturnError()
    {
        var metadata = new List<MetadataAttribute>
        {
            new(
                AttributeId: 1,
                Name: "Weight",
                DataType: AttributeDataType.Integer,
                Unit: "g",
                MinValue: 0,
                MaxValue: 10000,
                IsRequired: true
            ),
            new(
                AttributeId: 2,
                Name: "FatContent",
                DataType: AttributeDataType.Decimal,
                Unit: "%",
                MinValue: 0,
                MaxValue: 100,
                IsRequired: true
            )
        };

        SetupCache(metadata, categoryId: 1);

        var values = new List<EnumerationModel>
        {
            new(1, "30")
        };

        var result = await _sut.ValidateAndNormalizeAsync(1, values, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Validation.Errors.Should().Contain(e =>
            e.Code == MetadataValidationErrorCode.Required.ToApiCode() &&
            e.ValueId == 2);
    }

    [Theory]
    [InlineData("true", "true")]
    [InlineData("false", "false")]
    [InlineData("1", "true")]
    [InlineData("0", "false")]
    public async Task ValidateAndNormalizeAsync_BooleanAttribute_ShouldNormalize(
        string input,
        string expected)
    {
        var metadata = new List<MetadataAttribute>
        {
            new(
                AttributeId: 1,
                Name: "Organic",
                DataType: AttributeDataType.Boolean,
                Unit: null,
                MinValue: null,
                MaxValue: null,
                IsRequired: true
            )
        };

        SetupCache(metadata, categoryId: 1);

        var values = new List<EnumerationModel>
        {
            new(Id: 1, Value: input)
        };

        var result = await _sut.ValidateAndNormalizeAsync(1, values, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value![1].Should().Be(expected);
    }

    private void SetupCache(List<MetadataAttribute> metadata, int categoryId)
    {
        var cacheEntry = Mock.Of<ICacheEntry>();

        _cacheMock
            .Setup(x => x.CreateEntry(It.IsAny<object>()))
            .Returns(cacheEntry);

        object? cachedValue = metadata;
        _cacheMock
            .Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedValue))
            .Returns(false);

        _repositoryMock
            .Setup(x => x.GetMetadataSchemaAsync(categoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(metadata);
    }
}