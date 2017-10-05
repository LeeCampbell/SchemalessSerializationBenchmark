using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;

namespace SchemalessSerializationBenchmark.Serialization.MessagePackCsInfra
{
    public sealed class CustomCompositeResolver : IFormatterResolver
    {
        public static IFormatterResolver Instance = new CustomCompositeResolver();

        static readonly IFormatterResolver[] Resolvers = new[]
        {
            // resolver custom types first
            DateCustomResolver.Instance,
            
            // finaly use standard resolver
            StandardResolver.Instance
        };

        private CustomCompositeResolver()
        {
        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        private static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                foreach (var item in Resolvers)
                {
                    var f = item.GetFormatter<T>();
                    if (f != null)
                    {
                        formatter = f;
                        return;
                    }
                }
            }
        }
    }
}