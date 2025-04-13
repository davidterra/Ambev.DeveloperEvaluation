using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemCancelledEvent : INotification
    {
        public int SaleItemId { get; }

        public SaleItemCancelledEvent(int itemId)
        {
            SaleItemId = itemId;
        }
    }
}
