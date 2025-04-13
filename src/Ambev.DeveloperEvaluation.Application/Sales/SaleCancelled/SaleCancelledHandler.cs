using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Serilog;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleCancelled
{
    /// <summary>
    /// Handles the SaleCancelledNotification by simulating the publishing of the event to a message queue.
    /// This class is responsible for processing SaleCancelledEvent notifications and logging the event details
    /// for tracking purposes. It ensures that the event is serialized and logged appropriately.
    /// </summary>
    public class SaleCancelledHandler : INotificationHandler<SaleCancelledEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleCancelledHandler"/> class.
        /// </summary>
        public SaleCancelledHandler()
        {
            // Constructor logic can be added here if needed in the future.
        }

        /// <summary>
        /// Handles the SaleCancelledNotification event.
        /// Converts the notification data into a SaleCancelledEvent, serializes it, and logs the event details.
        /// This method simulates the publishing of the event to a message queue (e.g., RabbitMQ, Kafka).
        /// </summary>
        /// <param name="notification">The notification containing the sale details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A completed task.</returns>
        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            // Simulates publishing the event to a queue (e.g., RabbitMQ, Kafka)
            var message = JsonSerializer.Serialize(notification);

            Log.Information("SaleCancelled event published successfully. SaleId: {SaleId}, Message: {Message}", notification.SaleId, message);

            return Task.CompletedTask;
        }
    }

}
