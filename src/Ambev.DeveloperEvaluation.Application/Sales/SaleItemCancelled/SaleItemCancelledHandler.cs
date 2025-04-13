using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Serilog;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleItemCancelled
{
    /// <summary>
    /// Handles the <see cref="SaleItemCancelledEvent"/> by simulating the publishing of the event to a message queue.
    /// This class is responsible for logging the event details for tracking and debugging purposes.
    /// </summary>
    public class SaleItemCancelledHandler : INotificationHandler<SaleItemCancelledEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItemCancelledHandler"/> class.
        /// Currently, no specific initialization logic is required.
        /// </summary>
        public SaleItemCancelledHandler()
        {
            
        }

        /// <summary>
        /// Handles the <see cref="SaleItemCancelledEvent"/> notification.
        /// Converts the notification data into a JSON string, logs the event details, 
        /// and simulates publishing the event to a message queue.
        /// </summary>
        /// <param name="notification">The notification containing the details of the cancelled sale item.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A completed task representing the asynchronous operation.</returns>
        public Task Handle(SaleItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            // Simulates publishing the event to a queue (e.g., RabbitMQ, Kafka)
            var message = JsonSerializer.Serialize(notification);

            Log.Information("SaleItemCancelled event published successfully. ItemId: {ItemId} Message: {Message}", notification.SaleItemId, message);

            return Task.CompletedTask;
        }
    }

}
