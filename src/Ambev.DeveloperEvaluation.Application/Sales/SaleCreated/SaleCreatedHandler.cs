using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Serilog;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleCreated
{
    /// <summary>
    /// Handles the SaleCreatedEvent by simulating the publishing of the event to a message queue.
    /// This class is responsible for processing the event, serializing its data, and logging the operation.
    /// </summary>
    public class SaleCreatedHandler : INotificationHandler<SaleCreatedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleCreatedHandler"/> class.
        /// This constructor currently does not perform any specific initialization logic.
        /// </summary>
        public SaleCreatedHandler()
        {
            
        }

        /// <summary>
        /// Handles the SaleCreatedEvent by serializing the event data and logging the operation.
        /// This simulates publishing the event to a message queue (e.g., RabbitMQ, Kafka).
        /// </summary>
        /// <param name="notification">The notification containing the sale data.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A completed task representing the asynchronous operation.</returns>
        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Simulates publishing the event to a queue (e.g., RabbitMQ, Kafka)
            var message = JsonSerializer.Serialize(notification);

            Log.Information("SaleCreated event published successfully. SaleId: {SaleId}, Message: {Message}", notification.SaleId, message);

            return Task.CompletedTask;
        }
    }

}
