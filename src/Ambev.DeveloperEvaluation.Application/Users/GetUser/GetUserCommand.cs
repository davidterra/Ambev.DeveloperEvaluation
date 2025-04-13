using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Command for retrieving a user by their ID.
/// This command is used to encapsulate the data required to fetch a user from the system.
/// </summary>
public record GetUserCommand : IRequest<GetUserResult>
{
    /// <summary>
    /// The unique identifier of the user to retrieve.
    /// This ID is used to locate the specific user in the database or data source.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserCommand"/> class.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    public GetUserCommand(Guid id)
    {
        Id = id;
    }
}
