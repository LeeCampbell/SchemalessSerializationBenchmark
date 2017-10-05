using System;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public abstract class Message
    {
        /// <summary>
        /// The sequence id of the message.
        /// </summary>
        public long SequenceId { get; set; }

        /// <summary>
        /// The id of the message.
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// The id of the aggregate this message belongs to.
        /// </summary>
        public Guid AggregateId { get; set; }

        /// <summary>
        /// The time stamp the message was created.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Correlation id to map this message to the chain of commands/events that created it.
        /// </summary>
        public Guid CorrelationId { get; set; }

        /// <summary>
        /// The original MessageId of the message that caused the creation of this message.
        /// </summary>
        public Guid CausationId { get; set; }

        /// <summary>
        /// Either the <see cref="InstigatorId"/> of the command that instigated this message, or the Service identifier that instigated this event.
        /// </summary>
        public Guid InstigatorId { get; set; }

        /// <summary>
        /// Description of the type that instigated this chain of messages.
        /// </summary>
        public string InstigatorType { get; set; }

        /// <summary>
        /// The name of the instigator of this chain of messages.
        /// </summary>
        public string InstigatorName { get; set; }
    }
}