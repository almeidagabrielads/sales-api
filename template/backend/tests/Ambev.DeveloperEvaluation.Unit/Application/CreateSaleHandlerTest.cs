using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain;

using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Unit.Application;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using Bogus;

using FluentAssertions;

using NSubstitute;

using Xunit;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTest
{
     private readonly ISaleRepository saleRepository;
     private readonly ISaleItemRepository saleItemRepository;
     private readonly IMapper mapper;
     private readonly IMediator mediator;
     private readonly CreateSaleHandler handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTest"/> class.
    /// Sets up test dependencies and fake data.
    /// </summary>
     public CreateSaleHandlerTest()
    {
        this.saleRepository = Substitute.For<ISaleRepository>();
        this.saleItemRepository = Substitute.For<ISaleItemRepository>();
        this.mapper = Substitute.For<IMapper>();
        this.mediator = Substitute.For<IMediator>();
        this.handler = new CreateSaleHandler(this.saleRepository, this.saleItemRepository, this.mapper, this.mediator);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
     [Fact(DisplayName = "Given valid sale data When creating sale Then returns result with Id")]
     public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        CreateSaleCommand command = CreateSaleHandlerTestData.GenerateValidCommand();
        Sale sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            CustomerExternalId = command.CustomerExternalId,
            BranchExternalId = command.BranchExternalId,
            CreatedAt = DateTime.UtcNow,
            IsCancelled = false,
            Items = command.Items.Select(i => new SaleItem
            {
                ProductExternalId = i.ProductExternalId,
                Quantity = i.Quantity
            }).ToList()
        };

        CreateSaleResult result = new CreateSaleResult
        {
            Id = sale.Id
        };
        this.mapper.Map<Sale>(command).Returns(sale);
        this.mapper.Map<CreateSaleResult>(sale).Returns(result);

        this.saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        CreateSaleResult createSaleResult = await this.handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        await this.saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

     /// <summary>
     /// Tests that an invalid request (e.g. no items) throws a validation exception.
     /// </summary>
     [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
     public async Task Handle_InvalidRequest_ThrowsValidationException()
     {
        // Given
        CreateSaleCommand command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        Func<Task> act = async () => await this.handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
     }
}