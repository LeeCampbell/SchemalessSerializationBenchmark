using MessagePack.Formatters;

namespace SchemalessSerializationBenchmark.Serialization.MessagePackCsInfra
{
    public sealed class DateCustomResolver : MessagePack.IFormatterResolver
    {
        // Resolver should be singleton.
        public static MessagePack.IFormatterResolver Instance = new DateCustomResolver();

        private DateCustomResolver()
        {
        }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            // generic's static constructor should be minimized for reduce type generation size!
            // use outer helper method.
            static FormatterCache()
            {
                formatter = (IMessagePackFormatter<T>)DateCustomResolverGetFormatterHelper.GetFormatter(typeof(T));
            }
        }
    }
}