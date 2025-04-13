using Ambev.DeveloperEvaluation.Application.Users.ListUsers;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Users
{
    public static class ListUsersHandlerTestData
    {
        internal static ListUsersCommand GenerateValidCommand()
        {
            return new ListUsersCommand
            {
                OrderBy = "\"Username\"",
                Filters = new Dictionary<string, string>
                {
                    { "Status", "Active" },
                    { "Role", "Admin" }
                }
            };
        }

    }
}
