using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

/// <summary>
/// Handles the deletion of a sale item within a sale.
/// Validates the request, interacts with the repository to update the sale and its items,
/// and publishes an event upon successful deletion.
/// </summary>
public class DeleteSaleItemHandler : IRequestHandler<DeleteSaleItemCommand, DeleteSaleItemResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleItemHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository for managing sale data.</param>
    /// <param name="mediator">The mediator for publishing domain events.</param>
    public DeleteSaleItemHandler(
        ISaleRepository saleRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the <see cref="DeleteSaleItemCommand"/> request.
    /// Validates the command, retrieves the sale and item, marks the item as canceled,
    /// updates the sale and item in the repository, and publishes a cancellation event.
    /// </summary>
    /// <param name="request">The command containing the sale ID and item ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token to handle request cancellation.</param>
    /// <returns>
    /// A <see cref="DeleteSaleItemResponse"/> indicating the success of the delete operation.
    /// Throws <see cref="ValidationException"/> if validation fails.
    /// Throws <see cref="ResourceNotFoundException"/> if the sale or item is not found.
    /// </returns>
    public async Task<DeleteSaleItemResponse> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleItemValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
        {
            throw new ResourceNotFoundException("Sale not found", $"Sale with ID {request.Id} not found");
        }

        var item = sale.Items.FirstOrDefault(x => x.Id == request.ItemId);

        if (item == null)
        {
            throw new ResourceNotFoundException("Sale item not found", $"Sale item with ID {request.ItemId} not found");
        }

        item.SetAsCanceled();
        await _saleRepository.UpdateItemAsync(item);
        sale.SetAsUpdated();
        sale.Subtotal();
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleItemCancelledEvent(item.Id), cancellationToken);

        return new DeleteSaleItemResponse { Success = true };
    }
}
