using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discounts
{
    public sealed class NoDiscountSpecification : ISpecification<CartItem>
    {
        public bool IsSatisfiedBy(CartItem product)
        {
            return product.Quantity < 4 && product.DiscountPercent.Value > 0;
        }
    }
}
