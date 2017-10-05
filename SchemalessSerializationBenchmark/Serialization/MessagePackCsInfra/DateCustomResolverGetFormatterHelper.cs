using System;
using System.Collections.Generic;
using SchemalessSerializationBenchmark.Serialization.Contracts;

namespace SchemalessSerializationBenchmark.Serialization.MessagePackCsInfra
{
    internal static class DateCustomResolverGetFormatterHelper
    {
        // If type is concrete type, use type-formatter map
        static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
        {
            {typeof(Date), new DateFormatter()},
            // add more your own custom serializers.
        };

        internal static object GetFormatter(Type t)
        {
            object formatter;
            if (formatterMap.TryGetValue(t, out formatter))
            {
                return formatter;
            }

            //// If target type is generics, use MakeGenericType.
            //if (t.IsGenericParameter && t.GetGenericTypeDefinition() == typeof(ValueTuple<,>))
            //{
            //    return Activator.CreateInstance(typeof(ValueTupleFormatter<,>).MakeGenericType(t.GenericTypeArguments));
            //}

            // If type can not get, must return null for fallback mecanism.
            return null;
        }
    }
}