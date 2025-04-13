using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discounts
{
    public sealed class TwentyPercentDiscountSpecification : ISpecification<CartItem>
    {
        public bool IsSatisfiedBy(CartItem product)
        {
            return product.Quantity >= 10 && product.Quantity <= 20;
        }
    }
}
