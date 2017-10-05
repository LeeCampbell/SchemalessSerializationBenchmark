using System;
using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Attributes;
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
    public class DeserilaizationBenchmark
    {
        private readonly string _jsonInput;
        private readonly byte[] _bsonInput;
        private readonly MessagePackSerializer<MonthlyFeeChargedEvent> _msgPackserializer;
        private readonly MessagePackSerializer<MonthlyFeeChargedEventProxy> _msgPackProxySerializer;
        private readonly byte[] _fullMsgPackInput;
        private readonly byte[] _csPayload;
        private readonly byte[] _lz4CsPayload;

        public DeserilaizationBenchmark()
        {
            //JSON.NET Setup
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new DateJsonSerializer()
                }
            };
            _jsonInput = File.ReadAllText(@"Serialization\MonthlyFeeEvent.json");
            var monthlyFeeChargedEvent = JsonConvert.DeserializeObject<MonthlyFeeChargedEvent>(_jsonInput);
            var withoutRepaymentSchedule = JsonConvert.DeserializeObject<MonthlyFeeChargedEvent>(_jsonInput);
            withoutRepaymentSchedule.RepaymentSchedule = null;
            _jsonInput = JsonConvert.SerializeObject(monthlyFeeChargedEvent);   //Reformat as not pretty print
            _bsonInput = BsonSerialize(monthlyFeeChargedEvent);   //Reformat as not pretty print

            //MsgPack.Cli setup
            var context = new SerializationContext();
            context.Serializers.Register(new DateMsgPackSerializer(context));
            context.SerializationMethod = SerializationMethod.Map;
            _msgPackserializer = context.GetSerializer<MonthlyFeeChargedEvent>(context);
            _msgPackProxySerializer = context.GetSerializer<MonthlyFeeChargedEventProxy>(context);
            _fullMsgPackInput = _msgPackserializer.PackSingleObject(monthlyFeeChargedEvent);

            //MessagePack-CS set up
            CompositeResolver.RegisterAndSetAsDefault(
                CustomCompositeResolver.Instance,
                ContractlessStandardResolver.Instance
            );
            _csPayload = MessagePack.MessagePackSerializer.Serialize(monthlyFeeChargedEvent);
            _lz4CsPayload = MessagePack.LZ4MessagePackSerializer.Serialize(monthlyFeeChargedEvent);
        }

        [Benchmark(Baseline = true)]
        public TransactionRow Json_TypeDeseralization()
        {
            var evt = JsonConvert.DeserializeObject<MonthlyFeeChargedEvent>(_jsonInput);
            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        [Benchmark]
        public TransactionRow Json_JObjectDeseralization()
        {
            var jobj = JObject.Parse(_jsonInput);
            return new TransactionRow
            {
                BankingDate = jobj["BankingDate"].ToObject<Date>(),
                EffectiveDate = jobj["EffectiveDate"].ToObject<Date>(),
                TransactionDate = jobj["TransactionDate"].ToObject<DateTimeOffset>(),
                Amount = jobj["Amount"].ToObject<decimal>(),
                Category = (int)jobj["TransactionType"].ToObject<LedgerTransactionType>()
            };
        }
        [Benchmark]
        public TransactionRow Json_ProxyTypeDeseralization()
        {
            var evt = JsonConvert.DeserializeObject<MonthlyFeeChargedEventProxy>(_jsonInput);
            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        

        [Benchmark]
        public TransactionRow Bson_TypeDeseralization()
        {
            var evt = BsonDeserialize<MonthlyFeeChargedEvent>(_bsonInput);
            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        [Benchmark]
        public TransactionRow Bson_ProxyTypeDeseralization()
        {
            var evt = BsonDeserialize<MonthlyFeeChargedEventProxy>(_bsonInput);
            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        

        [Benchmark]
        public TransactionRow MsgPackCli_TypeDeseralization()
        {
            var stream = new MemoryStream(_fullMsgPackInput);
            var evt = _msgPackserializer.Unpack(stream);
            stream.Dispose();
            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        [Benchmark]
        public TransactionRow MsgPackCli_ProxyTypeDeseralization()
        {
            var stream = new MemoryStream(_fullMsgPackInput);
            var evt = _msgPackProxySerializer.Unpack(stream);
            stream.Dispose();
            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        

        [Benchmark]
        public TransactionRow MessagePack_TypeDeseralization()
        {
            var evt = MessagePack.MessagePackSerializer.Deserialize<MonthlyFeeChargedEvent>(_csPayload);
            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        [Benchmark]
        public TransactionRow MessagePack_ProxyTypeDeseralization()
        {
            var evt = MessagePack.MessagePackSerializer.Deserialize<MonthlyFeeChargedEventProxy>(_csPayload);

            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        

        [Benchmark]
        public TransactionRow MessagePackLz4_TypeDeseralization()
        {
            var evt = MessagePack.LZ4MessagePackSerializer.Deserialize<MonthlyFeeChargedEvent>(_lz4CsPayload);
            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        [Benchmark]
        public TransactionRow MessagePackLz4_ProxyTypeDeseralization()
        {
            var evt = MessagePack.LZ4MessagePackSerializer.Deserialize<MonthlyFeeChargedEventProxy>(_lz4CsPayload);

            return new TransactionRow
            {
                BankingDate = evt.BankingDate,
                EffectiveDate = evt.EffectiveDate,
                TransactionDate = evt.TransactionDate,
                Amount = evt.Amount,
                Category = (int)evt.TransactionType
            };
        }
        

        private static readonly JsonSerializer Serializer = new JsonSerializer();
        private static byte[] BsonSerialize<T>(T input)
        {
            var ms = new MemoryStream();
            using(var writer = new BsonDataWriter(ms))
            {
                Serializer.Serialize(writer, input);
            }
            return ms.ToArray();
        }
        private static T BsonDeserialize<T>(byte[] input)
        {
            var ms = new MemoryStream(input);
            using (var reader = new BsonDataReader(ms))
            {
                return Serializer.Deserialize<T>(reader);
            }
        }
    }
    public class TransactionRow
    {
        public Date BankingDate { get; set; }
        public Date EffectiveDate { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int Category { get; set; }
    }
}