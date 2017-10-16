using System;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    /// <summary>
    /// Defines properties common to all events.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// The sequence id of the event.
        /// </summary>
        long SequenceId { get; set; }

        /// <summary>
        /// The message id of the event.
        /// </summary>
        Guid MessageId { get; set; }

        /// <summary>
        /// The id of the aggregate this event belongs to.
        /// </summary>
        Guid AggregateId { get; set; }

        /// <summary>
        /// The time stamp the event was created.
        /// </summary>
        DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Correlation id to map this event to the chain of commands/events that created it.
        /// </summary>
        Guid CorrelationId { get; set; }

        /// <summary>
        /// The original MessageId of the message that caused the creation of this event.
        /// </summary>
        Guid CausationId { get; set; }

        /// <summary>
        /// Either the <see cref="InstigatorId"/> of the command that instigated this message, or the Service identifier that instigated this event.
        /// </summary>
        Guid InstigatorId { get; set; }

        /// <summary>
        /// Description of the type that instigated this chain of messages.
        /// </summary>
        string InstigatorType { get; set; }

        /// <summary>
        /// The name of the instigator of this chain of messages.
        /// </summary>
        string InstigatorName { get; set; }

        /// <summary>
        /// The timestamp from the command that caused the event.
        /// </summary>
        DateTimeOffset? CommandTimestamp { get; set; }
    }
}