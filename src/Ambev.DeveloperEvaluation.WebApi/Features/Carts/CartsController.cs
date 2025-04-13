using Ambev.DeveloperEvaluation.Application.Carts.CreateCartItem;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCartItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCartItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles HTTP GET requests to list carts with pagination, ordering, and optional filters.
    /// </summary>
    /// <param name="_page">The page number to retrieve. Defaults to 1.</param>
    /// <param name="_size">The number of items per page. Defaults to 10.</param>
    /// <param name="_order">The order by which the results should be sorted. Defaults to an empty string.</param>
    /// <param name="filters">Optional filters to apply to the cart list. Defaults to null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A paginated list of carts. If successful, returns a 200 OK status with the paginated data.
    /// If validation fails, returns a 400 Bad Request status with validation errors.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<ListCartsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListCarts(
    [FromQuery] int _page = 1,
    [FromQuery] int _size = 10,
    [FromQuery] string _order = "",
    [FromQuery] Dictionary<string, string>? filters = null,
    CancellationToken cancellationToken = default)
    {
        var request = new ListCartsRequest
        {
            Page = _page,
            Size = _size,
            OrderBy = _order,
            Filters = filters
        };

        var validator = new ListCartsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<ListCartsCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        var response = result.ProjectTo<ListCartsResponse>(_mapper.ConfigurationProvider);

        var paginatedList = await PaginatedList<ListCartsResponse>.CreateAsync(response, _page, _size);

        return OkPaginated(paginatedList);
    }

    /// <summary>
    /// Handles HTTP GET requests to retrieve a cart by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A response containing the cart details if found. If successful, returns a 200 OK status with the cart data.
    /// If validation fails, returns a 400 Bad Request status with validation errors.
    /// If the cart is not found, returns a 404 Not Found status.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetCartRequest { Id = id };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCartCommand>(request.Id);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<GetCartResponse>(result);

        return Ok(new ApiResponseWithData<GetCartResponse>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// Handles HTTP DELETE requests to delete a cart by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to be deleted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A response indicating the result of the operation. If successful, returns a 200 OK status with a success message.
    /// If validation fails, returns a 400 Bad Request status with validation errors.
    /// If the cart is not found, returns a 404 Not Found status.
    /// </returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCart([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCartCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok("Cart deleted successfully");
    }


    /// <summary>
    /// Handles HTTP POST requests to create a new cart item.
    /// </summary>
    /// <param name="request">The request object containing details of the cart item to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A response indicating the result of the operation. If successful, returns a 201 Created status with the created cart item data.
    /// If validation fails, returns a 400 Bad Request status with validation errors.
    /// </returns>
    [HttpPost("item")]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartItemResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCartItem([FromBody] CreateCartItemRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCartItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCartItemCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCartItemResponse>
        {
            Success = true,
            Message = "Cart item created successfully",
            Data = _mapper.Map<CreateCartItemResponse>(response)
        });
    }


    /// <summary>
    /// Handles HTTP DELETE requests to delete a cart item by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart item to be deleted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A response indicating the result of the operation. If successful, returns a 200 OK status with a success message.
    /// If validation fails, returns a 400 Bad Request status with validation errors.
    /// If the cart item is not found, returns a 404 Not Found status.
    /// </returns>
    [HttpDelete("item/{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCartItem([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new DeleteCartItemRequest { Id = id };
        var validator = new DeleteCartItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCartItemCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok("Cart item deleted successfully");
    }


    /// <summary>
    /// Handles HTTP PATCH requests to update the quantity of a cart item.
    /// </summary>
    /// <param name="id">The unique identifier of the cart item to be updated.</param>
    /// <param name="request">The request object containing the new quantity for the cart item.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A response indicating the result of the operation. If successful, returns a 200 OK status with the updated cart item data.
    /// If validation fails, returns a 400 Bad Request status with validation errors.
    /// </returns>
    [HttpPatch("item/{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCartItem([FromRoute] int id, [FromBody] UpdateCartItemRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var validator = new UpdateCartItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateCartItemCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);
        var data = _mapper.Map<UpdateCartItemResponse>(response);

        return Ok(data, "Cart item quantity updated successfully");
    }
}