using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent : INotification
    {
        public int SaleId { get; }

        public SaleModifiedEvent(int saleId)
        {
            SaleId = saleId;
        }
    }
}
