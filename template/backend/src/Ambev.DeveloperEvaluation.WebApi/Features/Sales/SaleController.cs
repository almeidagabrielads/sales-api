using System.Security.Claims;

using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

using Ambev.DeveloperEvaluation.WebApi.Common;

using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// /// Controller for managing sales operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SalesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    /// <param name="request">The sale creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created sale details.</returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        if (!this.User.Identity?.IsAuthenticated ?? true)
        {
            return this.Unauthorized();
        }

        CreateSaleRequestValidator validator = new();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return this.BadRequest(validationResult.Errors);
        }

        CreateSaleCommand command = this._mapper.Map<CreateSaleCommand>(request);
        CreateSaleResult response = await this._mediator.Send(command, cancellationToken);

        return this.Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = this._mapper.Map<CreateSaleResponse>(response),
        });
    }
    
    /// <summary>
    /// Updates sale.
    /// </summary>
    /// <param name="request">The sale update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The update sale details.</returns>
    [Authorize]
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        if (!this.User.Identity?.IsAuthenticated ?? true)
        {
            return this.Unauthorized();
        }

        UpdateSaleRequestValidator validator = new();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return this.BadRequest(validationResult.Errors);
        }

        UpdateSaleCommand command = this._mapper.Map<UpdateSaleCommand>(request);
        UpdateSaleResult response = await this._mediator.Send(command, cancellationToken);

        return this.Ok(new ApiResponseWithData<UpdateSaleResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = this._mapper.Map<UpdateSaleResponse>(response),
        });
    }
    
    // <summary>
    // Retrieves a sale by their ID.
    // </summary>
    // <param name="id">The unique identifier of the sale.</param>
    // <param name="cancellationToken">Cancellation token.</param>
    // <returns>The sale details if found.</returns>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        if (!this.User.Identity?.IsAuthenticated ?? true)
        {
            return this.Unauthorized();
        }
        
        GetSaleRequest request = new GetSaleRequest { Id = id };
        GetSaleRequestValidator validator = new();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return this.BadRequest(validationResult.Errors);
        }

        GetSaleCommand command = this._mapper.Map<GetSaleCommand>(request.Id);
        GetSaleResult response = await this._mediator.Send(command, cancellationToken);

        return this.Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = this._mapper.Map<GetSaleResponse>(response),
        });
    }
    
    /// <summary>
    /// Deletes a sale by their ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Success response if the sale was deleted.</returns>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        if (!this.User.Identity?.IsAuthenticated ?? true)
        {
            return this.Unauthorized();
        }
        
        DeleteSaleRequest request = new DeleteSaleRequest { Id = id };
        DeleteSaleRequestValidator validator = new();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return this.BadRequest(validationResult.Errors);
        }

        DeleteSaleCommand command = this._mapper.Map<DeleteSaleCommand>(request.Id);
        await this._mediator.Send(command, cancellationToken);

        return this.Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale deleted successfully",
        });
    }
}