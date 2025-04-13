using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Serilog;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleModified
{
    /// <summary>
    /// Handles the SaleModifiedEvent by simulating the publishing of the event to a message queue
    /// and logging the event details. This class is responsible for ensuring that the event is processed
    /// and its details are logged for auditing or debugging purposes.
    /// </summary>
    public class SaleModifiedHandler : INotificationHandler<SaleModifiedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleModifiedHandler"/> class.
        /// This class is designed to handle SaleModifiedEvent notifications and simulate
        /// their publishing to a message queue while logging the event details.
        /// </summary>
        public SaleModifiedHandler()
        {            
        }

        /// <summary>
        /// Processes the SaleModifiedEvent.
        /// Serializes the event data into JSON format and logs the details of the sale modification.
        /// This method simulates the publishing of the event to a message queue (e.g., RabbitMQ, Kafka).
        /// </summary>
        /// <param name="notification">The notification containing the details of the sale modification.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A completed task representing the asynchronous operation.</returns>
        public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            // Simulates publishing the event to a queue (e.g., RabbitMQ, Kafka)

            var message = JsonSerializer.Serialize(notification);

            Log.Information("SaleModified event published successfully. SaleId: {SaleId}", notification.SaleId, message);

            return Task.CompletedTask;
        }
    }

}
