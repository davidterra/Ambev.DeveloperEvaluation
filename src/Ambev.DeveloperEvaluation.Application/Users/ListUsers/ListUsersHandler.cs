using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Handler for processing ListUsersCommand requests.
/// This handler validates the command, retrieves the list of users from the repository,
/// and maps the result to the ListUsersResult model.
/// </summary>
public class ListUsersHandler : IRequestHandler<ListUsersCommand, IQueryable<ListUsersResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the ListUsersHandler class.
    /// </summary>
    /// <param name="userRepository">The user repository for accessing user data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
    public ListUsersHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListUsersCommand request.
    /// Validates the command, retrieves the filtered and ordered list of users,
    /// and maps the result to the ListUsersResult model.
    /// </summary>
    /// <param name="request">The ListUsersCommand containing filter and order parameters.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>A queryable collection of ListUsersResult containing user details.</returns>
    /// <exception cref="ValidationException">Thrown if the command validation fails.</exception>
    public async Task<IQueryable<ListUsersResult>> Handle(ListUsersCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListUsersCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var users = _userRepository.ListUsers(request.OrderBy, request.Filters);
        var result = users.ProjectTo<ListUsersResult>(_mapper.ConfigurationProvider);

        return result;
    }
}
