using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discounts
{
    public sealed class TenPercentDiscountSpecification : ISpecification<CartItem>
    {
        public bool IsSatisfiedBy(CartItem product)
        {
            return product.Quantity >= 4 && product.Quantity < 10;
        }
    }
}
