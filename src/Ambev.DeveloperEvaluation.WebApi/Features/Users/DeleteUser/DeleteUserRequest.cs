namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Request model for deleting a user.
/// This class is used to encapsulate the data required to delete a user from the system.
/// </summary>
public class DeleteUserRequest
{
    /// <summary>
    /// The unique identifier of the user to delete.
    /// This ID is used to locate and remove the user from the database.
    /// </summary>
    public Guid Id { get; set; }
}
