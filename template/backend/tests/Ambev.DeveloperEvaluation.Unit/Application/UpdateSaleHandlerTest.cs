using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
        using Ambev.DeveloperEvaluation.Domain.Entities;
        using Ambev.DeveloperEvaluation.Domain.Repositories;
        using Ambev.DeveloperEvaluation.Unit.Application.TestData;
        using Ambev.DeveloperEvaluation.Unit.Domain;
        using AutoMapper;
        using FluentAssertions;
        using FluentValidation;
        using MediatR;
        using NSubstitute;
        using Xunit;
        
        namespace Ambev.DeveloperEvaluation.Unit.Application;
        
        /// <summary>
        /// Contains unit tests for the <see cref="UpdateSaleHandler"/> class, verifying its behavior when handling update sale commands.
        /// </summary>
        public class UpdateSaleHandlerTest
        {
            private readonly ISaleRepository saleRepository;
            private readonly ISaleItemRepository saleItemRepository;
            private readonly IMapper mapper;
            private readonly IMediator mediator;
            private readonly UpdateSaleHandler handler;
        
            /// <summary>
            /// Initializes a new instance of the <see cref="UpdateSaleHandlerTest"/> class.
            /// Sets up the handler with mocked dependencies for testing.
            /// </summary>
            public UpdateSaleHandlerTest()
            {
                this.saleRepository = Substitute.For<ISaleRepository>();
                this.saleItemRepository = Substitute.For<ISaleItemRepository>();
                this.mapper = Substitute.For<IMapper>();
                this.mediator = Substitute.For<IMediator>();
                this.handler = new UpdateSaleHandler(this.saleRepository, this.saleItemRepository, this.mapper, this.mediator);
            }
        
            /// <summary>
            /// Verifies that a <see cref="ValidationException"/> is thrown when the command is missing required fields.
            /// </summary>
            [Fact(DisplayName = "Should throw ValidationException when command is invalid")]
            public async Task Handle_InvalidCommand_ThrowsValidationException()
            {
                // Arrange
                var invalidCommand = new UpdateSaleCommand(); // Empty command
        
                // Act
                Func<Task> act = async () => await this.handler.Handle(invalidCommand, CancellationToken.None);
        
                // Assert
                await act.Should().ThrowAsync<ValidationException>();
            }
        
            /// <summary>
            /// Verifies that an <see cref="InvalidOperationException"/> is thrown when the sale is not found in the repository.
            /// </summary>
            [Fact(DisplayName = "Should throw InvalidOperationException when sale does not exist")]
            public async Task Handle_SaleNotFound_ThrowsInvalidOperationException()
            {
                // Arrange
                var command = UpdateSaleHandlerTestData.GenerateValidCommand();
                this.saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                                   .Returns((Sale?)null);
        
                // Act
                Func<Task> act = async () => await this.handler.Handle(command, CancellationToken.None);
        
                // Assert
                await act.Should().ThrowAsync<InvalidOperationException>()
                         .WithMessage($"Sale with Id {command.Id} does not exist");
            }
        
            /// <summary>
            /// Verifies that an existing sale is updated and the correct result is returned.
            /// </summary>
            [Fact(DisplayName = "Should update and return updated sale result successfully")]
            public async Task Handle_ValidCommand_ReturnsUpdatedResult()
            {
                // Arrange
                var command = UpdateSaleHandlerTestData.GenerateValidCommand();
                var existingSale = UpdateSaleHandlerTestData.GenerateExistingSale(command.Id);
                var mappedItems = UpdateSaleHandlerTestData.GenerateMappedSaleItems(command.NewItems);
                var updatedSale = UpdateSaleHandlerTestData.GenerateExistingSale(command.Id);
                var expectedResult = UpdateSaleHandlerTestData.GenerateResult(command.Id);
        
                this.saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                                   .Returns(existingSale);
        
                this.mapper.Map<List<SaleItem>>(command.NewItems)
                           .Returns(mappedItems);
        
                this.saleRepository.UpdateAsync(existingSale, Arg.Any<CancellationToken>())
                                   .Returns(updatedSale);
        
                this.mapper.Map<UpdateSaleResult>(updatedSale)
                           .Returns(expectedResult);
        
                // Act
                var result = await this.handler.Handle(command, CancellationToken.None);
        
                // Assert
                result.Should().NotBeNull();
                result.Id.Should().Be(command.Id);
            }
        }