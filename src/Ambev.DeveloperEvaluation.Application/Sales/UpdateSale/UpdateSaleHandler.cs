using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handles the update of an existing sale by processing the provided command,
/// validating the input, ensuring the branch exists, and updating the sale details.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">Repository for managing sales.</param>
    /// <param name="branchRepository">Repository for managing branches.</param>
    /// <param name="mediator">Mediator for publishing domain events.</param>
    /// <param name="mapper">Mapper for converting between entities and DTOs.</param>
    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IBranchRepository branchRepository,
        IMediator mediator,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _branchRepository = branchRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Processes the <see cref="UpdateSaleCommand"/> to update an existing sale.
    /// </summary>
    /// <param name="command">The command containing sale update details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>The result of the sale update process.</returns>
    /// <exception cref="ValidationException">Thrown if the command validation fails.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown if the sale or branch does not exist.</exception>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await EnsureBranchExistsAsync(command.BranchId, cancellationToken);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
        {
            throw new ResourceNotFoundException("Sale not found", $"Sale with ID {command.Id} not found.");
        }

        sale.BranchId = command.BranchId;
        sale.SetAsUpdated();
        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleModifiedEvent(updatedSale.Id));

        return _mapper.Map<UpdateSaleResult>(updatedSale);
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
}
