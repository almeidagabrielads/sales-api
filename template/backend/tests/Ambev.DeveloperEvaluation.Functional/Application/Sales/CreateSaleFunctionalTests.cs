using System.Text;

using Ambev.DeveloperEvaluation.Functional.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Sales.CreateSale;

using Newtonsoft.Json;

namespace Ambev.DeveloperEvaluation.Functional.Application.Sales;

using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Functional.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

[Collection("CustomWebApplicationFactory")]
public class CreateSaleFunctionalTests
{
    private readonly HttpClient client;

    public CreateSaleFunctionalTests(CustomWebApplicationFactory factory)
    {
        this.client = factory.CreateClient();
    }

    [Fact(DisplayName = "POST /sales should create a sale and return 201")]
    public async Task Given_ValidSale_When_Posted_Then_ShouldReturnCreated()
    {
        // Arrange
        var command = CreateSaleCommandTestData.GenerateValidCommand();
        var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        // Act
        var response = await this.client.PostAsync("/sales", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact(DisplayName = "POST /sales with invalid body should return 400 Bad Request")]
    public async Task Given_InvalidSale_When_Posted_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var invalidCommand = new CreateSaleCommand(); 
        var content = new StringContent(JsonConvert.SerializeObject(invalidCommand), Encoding.UTF8, "application/json");

        // Act
        var response = await this.client.PostAsync("/sales", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact(DisplayName = "POST /sales without auth should return 401 Unauthorized")]
    public async Task Given_ValidSale_WithoutAuth_When_Posted_Then_ShouldReturnUnauthorized()
    {
        // Arrange
        var unauthenticatedClient = new HttpClient(); 
        var command = CreateSaleCommandTestData.GenerateValidCommand();
        var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        // Act
        var response = await unauthenticatedClient.PostAsync("http://localhost/sales", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}