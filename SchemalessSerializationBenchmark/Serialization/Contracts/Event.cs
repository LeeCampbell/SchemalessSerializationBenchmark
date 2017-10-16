using System;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public abstract class Event : Message, IEvent
    {
        /// <summary>
        /// The timestamp from the command that caused the event.
        /// </summary>
        public DateTimeOffset? CommandTimestamp { get; set; }
    }
}