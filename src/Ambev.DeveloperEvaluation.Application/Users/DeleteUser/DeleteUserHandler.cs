using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests.
/// This handler validates the command and interacts with the user repository
/// to delete a user by their unique identifier.
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler.
    /// </summary>
    /// <param name="userRepository">The user repository for user data operations.</param>
    public DeleteUserHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request.
    /// Validates the command, attempts to delete the user, and returns the result.
    /// </summary>
    /// <param name="request">The DeleteUser command containing the user ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token to handle request cancellation.</param>
    /// <returns>
    /// A <see cref="DeleteUserResponse"/> indicating the success of the delete operation.
    /// Throws <see cref="ValidationException"/> if validation fails.
    /// Throws <see cref="KeyNotFoundException"/> if the user is not found.
    /// </returns>
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteUserValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _userRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"User with ID {request.Id} not found");

        return new DeleteUserResponse { Success = true };
    }
}
