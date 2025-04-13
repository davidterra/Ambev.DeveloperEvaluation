using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent : INotification
    {
        public int SaleId { get; }

        public SaleCancelledEvent(int saleId)
        {
            SaleId = saleId;
        }
    }
}
