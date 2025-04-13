using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handles the creation of a new sale by processing the provided command, validating the input,
/// ensuring the branch and cart exist, and mapping the cart to a sale.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ISaleNumberGeneratorService _saleNumberGeneratorService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">Repository for managing sales.</param>
    /// <param name="branchRepository">Repository for managing branches.</param>
    /// <param name="cartRepository">Repository for managing carts.</param>
    /// <param name="saleNumberGeneratorService">Service for generating unique sale numbers.</param>
    /// <param name="mediator">Mediator for publishing domain events.</param>
    /// <param name="mapper">Mapper for converting between entities and DTOs.</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IBranchRepository branchRepository,
        ICartRepository cartRepository,
        ISaleNumberGeneratorService saleNumberGeneratorService,
        IMediator mediator,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _branchRepository = branchRepository;
        _cartRepository = cartRepository;
        _saleNumberGeneratorService = saleNumberGeneratorService;
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Processes the <see cref="CreateSaleCommand"/> to create a new sale.
    /// </summary>
    /// <param name="command">The command containing sale creation details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>The result of the sale creation process.</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        await ValidateCommandAsync(command, cancellationToken);
        await EnsureBranchExistsAsync(command.BranchId, cancellationToken);
        var cart = await GetCartAsync(command.CartId, cancellationToken);
        ValidateCart(cart);
        var sale = CreateSaleFromCart(cart, command);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        cart.SetAsConverted();
        await _cartRepository.UpdateAsync(cart, cancellationToken);

        await _mediator.Publish(new SaleCreatedEvent(createdSale.Id));

        return _mapper.Map<CreateSaleResult>(createdSale);
    }

    /// <summary>
    /// Validates the <see cref="CreateSaleCommand"/> using the provided validator.
    /// </summary>
    /// <param name="command">The command to validate.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <exception cref="ValidationException">Thrown if the command is invalid.</exception>
    private async Task ValidateCommandAsync(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }

    /// <summary>
    /// Ensures that the branch with the specified ID exists.
    /// </summary>
    /// <param name="branchId">The ID of the branch to check.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <exception cref="ResourceNotFoundException">Thrown if the branch does not exist.</exception>
    private async Task EnsureBranchExistsAsync(int branchId, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(branchId, cancellationToken);
        if (branch == null)
        {
            throw new ResourceNotFoundException("Branch", $"Branch with ID {branchId} not found.");
        }
    }

    /// <summary>
    /// Retrieves the cart with the specified ID.
    /// </summary>
    /// <param name="cartId">The ID of the cart to retrieve.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>The retrieved cart.</returns>
    /// <exception cref="ResourceNotFoundException">Thrown if the cart does not exist.</exception>
    private async Task<Cart> GetCartAsync(int cartId, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId, cancellationToken);
        if (cart == null)
        {
            throw new ResourceNotFoundException("Cart", $"Cart with ID {cartId} not found.");
        }
        return cart;
    }

    /// <summary>
    /// Validates the cart to ensure it is not canceled or empty.
    /// </summary>
    /// <param name="cart">The cart to validate.</param>
    /// <exception cref="InvalidOperationException">Thrown if the cart is canceled or empty.</exception>
    private void ValidateCart(Cart cart)
    {
        if (cart.IsCancelled() || cart.Items.All(x => x.IsCancelled()))
        {
            throw new InvalidOperationException("Cart is cancelled or empty.");
        }
    }

    /// <summary>
    /// Creates a new sale entity from the provided cart.
    /// </summary>
    /// <param name="cart">The cart to convert into a sale.</param>
    /// <param name="command">The command containing additional sale details.</param>
    /// <returns>The created sale entity.</returns>
    private Sale CreateSaleFromCart(Cart cart, CreateSaleCommand command)
    {
        var sale = _mapper.Map<Sale>(cart);
        sale.Number = _saleNumberGeneratorService.Generate();
        sale.BranchId = command.BranchId;
        sale.Subtotal();
        return sale;
    }
}
