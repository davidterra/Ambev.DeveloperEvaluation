using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;


/// <summary>
/// Handles the retrieval of a cart by its ID.
/// </summary>
public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartHandler"/> class.
    /// </summary>
    /// <param name="cartRepository">The repository to access cart data.</param>
    /// <param name="mapper">The mapper to transform domain models into result models.</param>
    public GetCartHandler(
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the request to retrieve a cart by its ID.
    /// </summary>
    /// <param name="request">The command containing the cart ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the cart details.</returns>
    /// <exception cref="ValidationException">Thrown when the request validation fails.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown when the cart is not found.</exception>
    public async Task<GetCartResult> Handle(GetCartCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart == null)
            throw new ResourceNotFoundException("Cart not found", $"Cart with ID {request.Id} not found");

        return _mapper.Map<GetCartResult>(cart);
    }
}
