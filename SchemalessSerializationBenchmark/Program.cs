using System;
using System.IO;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace SchemalessSerializationBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkSwitcher.FromTypes(new[]
            //        {
            //            typeof(DeserilaizationBenchmark),
            //        }
            //    )
            //    .Run(config: ManualConfig.Create(DefaultConfig.Instance)
            //        .With(Job.Clr)
            //        .With(Job.Core)
            //    );

            RunPerformanceBenchmarks();

            PrintFileSizeComparisons();
        }

        private static void PrintFileSizeComparisons()
        {
            //TODO: Print file size comparisons 
            var jsonLen = new FileInfo(@"Serialization\MonthlyFeeEvent.json").Length;
            var bsonLen = new FileInfo(@"Serialization\MonthlyFeeEvent.bson").Length;
            var msgPackLen = new FileInfo(@"Serialization\MonthlyFeeEvent.msgpack").Length;
            var messagePackLen = new FileInfo(@"Serialization\MonthlyFeeEvent.messagepack").Length;
            var messagePackLz4Len = new FileInfo(@"Serialization\MonthlyFeeEvent.messagepack.lz4").Length;

            Console.WriteLine();
            Console.WriteLine("File sizes:");
            Console.WriteLine($"  JSON (indented) {jsonLen,20}B, {Scaled(jsonLen, jsonLen)}");
            Console.WriteLine($"  BSON            {bsonLen,20}B, {Scaled(bsonLen, jsonLen)}");
            Console.WriteLine($"  MsgPack.Cli     {msgPackLen,20}B, {Scaled(msgPackLen, jsonLen)}");
            Console.WriteLine($"  MessagePack     {messagePackLen,20}B, {Scaled(messagePackLen, jsonLen)}");
            Console.WriteLine($"  MessagePack Lz4 {messagePackLz4Len,20}B, {Scaled(messagePackLz4Len, jsonLen)}");

        }

        private static string Scaled(double value, double baseline)
        {
            return $"{value / baseline,4:N2}x / {baseline / value,4:N2}x";
        }

        private static void RunPerformanceBenchmarks()
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
                .With(Job.Clr)
                .With(Job.Core);
            var summary = BenchmarkRunner.Run<DeserilaizationBenchmark>(config);
        }
    }
}
