namespace Ambev.DeveloperEvaluation.Domain.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public string Type = "ResourceNotFound";
        public string Error { get; }

        public ResourceNotFoundException(string error, string message) : base(message)
        {
            Error = error;
        }
    }
}
