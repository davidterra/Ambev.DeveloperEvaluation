using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handles the deletion of a sale by processing the DeleteSaleCommand.
/// This class validates the command, interacts with the sale repository to retrieve and update the sale,
/// and publishes a SaleCancelledEvent upon successful deletion.
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository for accessing and managing sale data.</param>
    /// <param name="mediator">The mediator for publishing domain events.</param>
    public DeleteSaleHandler(
        ISaleRepository saleRepository,
        IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the DeleteSaleCommand request.
    /// Validates the command, retrieves the sale, marks it as canceled, updates it in the repository,
    /// and publishes a SaleCancelledEvent.
    /// </summary>
    /// <param name="request">The DeleteSaleCommand containing the sale ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token to handle request cancellation.</param>
    /// <returns>
    /// A <see cref="DeleteSaleResponse"/> indicating the success of the delete operation.
    /// Throws <see cref="ValidationException"/> if validation fails.
    /// Throws <see cref="ResourceNotFoundException"/> if the sale is not found.
    /// </returns>
    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
        {
            throw new ResourceNotFoundException("Sale not found", $"Sale with ID {request.Id} not found");
        }

        sale.SetAsCanceled();
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleCancelledEvent(sale.Id), cancellationToken);

        return new DeleteSaleResponse { Success = true };
    }
}
