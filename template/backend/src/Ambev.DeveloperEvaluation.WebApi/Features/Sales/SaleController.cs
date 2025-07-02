using System.Security.Claims;

using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

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
    public async Task<IActionResult> CreateUser([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
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
            Message = "User created successfully",
            Data = this._mapper.Map<CreateSaleResponse>(response),
        });
    }
}