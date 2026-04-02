namespace GroceryStore.Tests.Infrastructure.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Database.Entities.User;
using FluentAssertions;
using GroceryStore.Infrastructure.Services;
using Microsoft.Extensions.Options;
using Shared.Options;

public class TokenServiceTests
{
    private readonly TokenService _sut;
    private readonly JwtOptions _jwtOptions;

    public TokenServiceTests()
    {
        _jwtOptions = new JwtOptions
        {
            SecretKey = "SuperSecretKeyForTestingPurposes123456",
            Issuer = "test-issuer",
            Audience = "test-audience",
            ExpirationMinutes = 60,
            RefreshTokenExpirationDays = 7
        };

        var options = Options.Create(_jwtOptions);
        _sut = new TokenService(options);
    }

    [Fact]
    public void GenerateAccessToken_ShouldReturnValidJwt()
    {
        var user = new AppUser
        {
            Id = "TestUser",
            Email = "test@user.com",
            FirstName = "TestFirstName",
            LastName = "TestLastName",
        };

        var claims = new List<string> { "User" };

        var token = _sut.GenerateAccessToken(user, claims);

        token.Should().NotBeNullOrEmpty();

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Claims.Should().Contain(c =>
            c.Type == JwtRegisteredClaimNames.Email &&
            c.Value == user.Email);
        jwtToken.Claims.Should().Contain(c =>
            c.Type == ClaimTypes.Role &&
            c.Value == "User");
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData("User")]
    public void GenerateAccessToken_ShouldContainCorrectRole(string role)
    {
        var user = new AppUser { Id = "1", Email = "test@test.com" };
        var claims = new List<string> { role };

        var token = _sut.GenerateAccessToken(user, claims);

        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == role);
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnUniqueTokens()
    {
        var token1 = _sut.GenerateRefreshToken();
        var token2 = _sut.GenerateRefreshToken();

        token1.Token.Should().NotBe(token2.Token);
        token1.Token.Should().HaveLength(44);
        token1.ExpiryTime.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_ShouldExtractClaims()
    {
        var user = new AppUser
        {
            Id = "TestUser",
            Email = "test@user.com",
            FirstName = "TestFirstName",
            LastName = "TestLastName",
        };

        var roles = new List<string> { "Admin" };
        var token = _sut.GenerateAccessToken(user, roles);

        var principal = _sut.GetPrincipalFromExpiredToken(token);

        principal.Should().NotBeNull();
        principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            .Should().Be(user.Id);
        principal.IsInRole("Admin").Should().BeTrue();
    }
}