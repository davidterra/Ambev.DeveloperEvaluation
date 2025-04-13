using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discounts
{
    public sealed class QuantityLimitSpecification : ISpecification<CartItem>
    {
        public bool IsSatisfiedBy(CartItem product)
        {
            return product.Quantity > 20;
        }
    }
}
