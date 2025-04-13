using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

/// <summary>
/// Handles the request to list carts with optional sorting and filtering.
/// </summary>
public class ListCartsHandler : IRequestHandler<ListCartsCommand, IQueryable<ListCartsResult>>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCartsHandler"/> class.
    /// </summary>
    /// <param name="cartRepository">The repository to access cart data.</param>
    /// <param name="mapper">The mapper to map domain entities to DTOs.</param>
    public ListCartsHandler(
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the request to list carts, applying validation, sorting, and filtering.
    /// </summary>
    /// <param name="request">The command containing pagination, sorting, and filtering options.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A queryable collection of <see cref="ListCartsResult"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the request validation fails.</exception>
    public async Task<IQueryable<ListCartsResult>> Handle(ListCartsCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListCartsCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var carts = _cartRepository.ListCarts(request.OrderBy, request.Filters);
        var result = carts.ProjectTo<ListCartsResult>(_mapper.ConfigurationProvider);
    
        return result;
    }
}
