using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent : INotification
    {
        public int SaleId { get; }

        public SaleCreatedEvent(int saleId)
        {
            SaleId = saleId;
        }
    }
}
