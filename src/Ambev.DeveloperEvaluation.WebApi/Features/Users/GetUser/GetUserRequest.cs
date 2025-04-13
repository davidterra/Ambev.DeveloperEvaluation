namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Represents the request model for retrieving a user by their unique identifier.
/// </summary>
public class GetUserRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user to retrieve.
    /// </summary>
    public Guid Id { get; set; }
}
