using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Sales.Services
{
    /// <summary>
    /// Service responsible for generating unique sale numbers.
    /// </summary>
    public class SaleNumberGeneratorService : ISaleNumberGeneratorService
    {
        /// <summary>
        /// Generates a unique sale number in the format "SALE-yyyyMMdd-XXXXXX".
        /// The sale number includes the current UTC date and a 6-character uppercase GUID segment.
        /// </summary>
        /// <returns>A unique sale number string.</returns>
        public string Generate()
        {
            return $"SALE-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }
    }
}
