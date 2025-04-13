using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Handler for processing UpdateUserCommand requests.
/// </summary>
/// <remarks>
/// This handler is responsible for updating user details in the system. 
/// It validates the input command, checks for the existence of the user, 
/// verifies the password, and updates the user information in the repository.
/// </remarks>
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    /// Initializes a new instance of UpdateUserHandler.
    /// </summary>
    /// <param name="userRepository">The user repository for accessing user data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping between models.</param>
    /// <param name="passwordHasher">The password hasher for verifying and hashing passwords.</param>
    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Handles the UpdateUserCommand request.
    /// </summary>
    /// <param name="command">The UpdateUser command containing user details to update.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>The updated user details as an <see cref="UpdateUserResult"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the command validation fails or input data is invalid.</exception>
    /// <exception cref="ResourceNotFoundException">Thrown when the user to update is not found.</exception>
    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user == null)
        {
            throw new ResourceNotFoundException("User not found", $"User with email {command.Email} does not exist");
        }

        if (user.Id != command.Id)
        {
            throw new ValidationException(new[] { new ValidationFailure("Email", $"User with email {command.Email} already exists") { ErrorCode = "Invalid input data" } });
        }

        if (!_passwordHasher.VerifyPassword(command.Password, user.Password))
        {
            throw new ValidationException(new[] { new ValidationFailure("Password", "The provided password is incorrect") { ErrorCode = "Invalid input data" } });
        }

        user = _mapper.Map<User>(command);
        user.Password = _passwordHasher.HashPassword(command.Password);

        var updatedUser = await _userRepository.UpdateAsync(user, cancellationToken);
        var result = _mapper.Map<UpdateUserResult>(updatedUser);
        return result;
    }
}
