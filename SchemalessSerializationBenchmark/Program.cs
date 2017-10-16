using System;
using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using MessagePack.Resolvers;
using MsgPack.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using SchemalessSerializationBenchmark.Serialization;
using SchemalessSerializationBenchmark.Serialization.Contracts;
using SchemalessSerializationBenchmark.Serialization.MessagePackCliInfra;
using SchemalessSerializationBenchmark.Serialization.MessagePackCsInfra;
using SchemalessSerializationBenchmark.Serialization.NewtonSoftJsonInfra;

namespace SchemalessSerializationBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //RedactGuids();
            //Translator.GenerateOtherFiles();

            RunPerformanceBenchmarks();
            PrintFileSizeComparisons();
        }



        private static void PrintFileSizeComparisons()
        {
            var jsonLen = new FileInfo(@"Serialization\MonthlyFeeEvent.json").Length;
            var jsonIndentedLen = PrettyPrintJson(@"Serialization\MonthlyFeeEvent.json").Length;
            var bsonLen = new FileInfo(@"Serialization\MonthlyFeeEvent.bson").Length;
            var msgPackLen = new FileInfo(@"Serialization\MonthlyFeeEvent.msgpack").Length;
            var messagePackLen = new FileInfo(@"Serialization\MonthlyFeeEvent.messagepack").Length;
            var messagePackLz4Len = new FileInfo(@"Serialization\MonthlyFeeEvent.messagepack.lz4").Length;

            Console.WriteLine();
            Console.WriteLine("File sizes:");
            Console.WriteLine($"  JSON            {jsonLen,20}B, {Scaled(jsonLen, jsonLen)}");
            Console.WriteLine($"  JSON (indented) {jsonIndentedLen,20}B, {Scaled(jsonIndentedLen, jsonLen)}");
            Console.WriteLine($"  BSON            {bsonLen,20}B, {Scaled(bsonLen, jsonLen)}");
            Console.WriteLine($"  MsgPack.Cli     {msgPackLen,20}B, {Scaled(msgPackLen, jsonLen)}");
            Console.WriteLine($"  MessagePack     {messagePackLen,20}B, {Scaled(messagePackLen, jsonLen)}");
            Console.WriteLine($"  MessagePack Lz4 {messagePackLz4Len,20}B, {Scaled(messagePackLz4Len, jsonLen)}");
        }

        private static string PrettyPrintJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JToken.Parse(json).ToString(Formatting.Indented);
        }

        private static string Scaled(double value, double baseline)
        {
            return $"{value / baseline,4:N2}x  ({baseline / value,4:N2}x smaller)";
        }

        private static void RunPerformanceBenchmarks()
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
                .With(Job.Clr)
                .With(Job.Core);
            var summary = BenchmarkRunner.Run<DeserilaizationBenchmark>(config);
        }


        private static void RedactGuids()
        {
            //JSON.NET Setup
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new DateJsonSerializer()
                }
            };
            var jsonInput = File.ReadAllText(@"Serialization\MonthlyFeeEvent.json");
            var monthlyFeeChargedEvent = JsonConvert.DeserializeObject<MonthlyFeeChargedEvent>(jsonInput);
            monthlyFeeChargedEvent.TransactionId = Guid.NewGuid();
            monthlyFeeChargedEvent.AggregateId = Guid.NewGuid();
            monthlyFeeChargedEvent.CausationId = Guid.NewGuid();
            monthlyFeeChargedEvent.CorrelationId = Guid.NewGuid();
            monthlyFeeChargedEvent.CustomerId = Guid.NewGuid();
            monthlyFeeChargedEvent.FundingStoreId = Guid.NewGuid();
            monthlyFeeChargedEvent.MessageId = Guid.NewGuid();
            monthlyFeeChargedEvent.InstigatorId = Guid.NewGuid();
            monthlyFeeChargedEvent.OriginatingStoreId = Guid.NewGuid();
            foreach (var scheduledRepayment in monthlyFeeChargedEvent.RepaymentSchedule)
            {
                scheduledRepayment.Id = Guid.NewGuid();
            }

            jsonInput = JsonConvert.SerializeObject(monthlyFeeChargedEvent);   //Reformat as not pretty print
            File.WriteAllText(@"Serialization\MonthlyFeeEvent.json", jsonInput);
            var fi = new FileInfo(@"Serialization\MonthlyFeeEvent.json");
            Console.WriteLine(fi.FullName);
            Console.ReadLine();
        }

        private class Translator
        {
            public static void GenerateOtherFiles()
            {
                //JSON.NET Setup
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter>
                    {
                        new DateJsonSerializer()
                    }
                };
                var jsonInput = File.ReadAllText(@"Serialization\MonthlyFeeEvent.json");
                var monthlyFeeChargedEvent = JsonConvert.DeserializeObject<MonthlyFeeChargedEvent>(jsonInput);


                var bsonInput = BsonSerialize(monthlyFeeChargedEvent);
                File.WriteAllBytes(@"Serialization\MonthlyFeeEvent.bson", bsonInput);


                //MsgPack.Cli setup
                var context = new SerializationContext();
                context.Serializers.Register(new DateMsgPackSerializer(context));
                context.SerializationMethod = SerializationMethod.Map;
                var msgPackserializer = context.GetSerializer<MonthlyFeeChargedEvent>(context);
                var fullMsgPackInput = msgPackserializer.PackSingleObject(monthlyFeeChargedEvent);
                File.WriteAllBytes(@"Serialization\MonthlyFeeEvent.msgpack", fullMsgPackInput);

                //MessagePack-CS set up
                CompositeResolver.RegisterAndSetAsDefault(
                    CustomCompositeResolver.Instance,
                    ContractlessStandardResolver.Instance
                );
                var csPayload = MessagePack.MessagePackSerializer.Serialize(monthlyFeeChargedEvent);
                File.WriteAllBytes(@"Serialization\MonthlyFeeEvent.MessagePack", csPayload);
                var lz4CsPayload = MessagePack.LZ4MessagePackSerializer.Serialize(monthlyFeeChargedEvent);
                File.WriteAllBytes(@"Serialization\MonthlyFeeEvent.MessagePack.lz4", lz4CsPayload);
            }

            private static readonly JsonSerializer Serializer = new JsonSerializer();
            private static byte[] BsonSerialize<T>(T input)
            {
                var ms = new MemoryStream();
                using (var writer = new BsonDataWriter(ms))
                {
                    Serializer.Serialize(writer, input);
                }
                return ms.ToArray();
            }
        }
    }
}
