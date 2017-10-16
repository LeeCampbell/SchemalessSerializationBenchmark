# SchemalessSerializationBenchmark
Benchmarks of various Schemaless Serialization formats and libraries

Referenced in the post https://leecampbell.com/2017/10/04/eventsourcing-with-messagepack-instead-of-json/

Benchmark takes about 8min to run and produces results similar to the following


``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Threshold 2 (10.0.10586)
Processor=Intel Core i5-4690 CPU 3.50GHz (Haswell), ProcessorCount=4
Frequency=3410090 Hz, Resolution=293.2474 ns, Timer=TSC
  [Host] : .NET Framework 4.6.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.6.1590.0
  Clr    : .NET Framework 4.6.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.6.1590.0
  Core   : .NET Core 1.1.2 (Framework 4.6.25211.01), 64bit RyuJIT


```
 |                                 Method |  Job | Runtime |       Mean |     Error |    StdDev | Scaled |
 |--------------------------------------- |----- |-------- |-----------:|----------:|----------:|-------:|
 |                Json_TypeDeseralization |  Clr |     Clr | 149.696 us | 0.5023 us | 0.4453 us |   1.00 |
 |             Json_JObjectDeseralization |  Clr |     Clr | 115.874 us | 0.7197 us | 0.6732 us |   0.77 |
 |           Json_ProxyTypeDeseralization |  Clr |     Clr |  50.584 us | 0.2629 us | 0.2459 us |   0.34 |
 |                Bson_TypeDeseralization |  Clr |     Clr | 193.824 us | 1.0551 us | 0.9869 us |   1.29 |
 |           Bson_ProxyTypeDeseralization |  Clr |     Clr |  57.429 us | 0.4736 us | 0.4430 us |   0.38 |
 |          MsgPackCli_TypeDeseralization |  Clr |     Clr |  93.432 us | 0.3112 us | 0.2759 us |   0.62 |
 |     MsgPackCli_ProxyTypeDeseralization |  Clr |     Clr |  44.810 us | 0.3619 us | 0.3208 us |   0.30 |
 |         MessagePack_TypeDeseralization |  Clr |     Clr |  28.034 us | 0.2316 us | 0.2167 us |   0.19 |
 |    MessagePack_ProxyTypeDeseralization |  Clr |     Clr |   4.728 us | 0.0313 us | 0.0293 us |   0.03 |
 |      MessagePackLz4_TypeDeseralization |  Clr |     Clr |  27.947 us | 0.1191 us | 0.1056 us |   0.19 |
 | MessagePackLz4_ProxyTypeDeseralization |  Clr |     Clr |   4.384 us | 0.0359 us | 0.0336 us |   0.03 |
 |                Json_TypeDeseralization | Core |    Core | 158.694 us | 1.3418 us | 1.2551 us |   1.00 |
 |             Json_JObjectDeseralization | Core |    Core | 117.989 us | 0.6825 us | 0.6051 us |   0.74 |
 |           Json_ProxyTypeDeseralization | Core |    Core |  50.296 us | 0.3407 us | 0.3020 us |   0.32 |
 |                Bson_TypeDeseralization | Core |    Core | 178.498 us | 2.1330 us | 1.9952 us |   1.12 |
 |           Bson_ProxyTypeDeseralization | Core |    Core |  62.148 us | 0.3210 us | 0.3002 us |   0.39 |
 |          MsgPackCli_TypeDeseralization | Core |    Core | 185.956 us | 1.9603 us | 1.8337 us |   1.17 |
 |     MsgPackCli_ProxyTypeDeseralization | Core |    Core |  46.308 us | 0.2335 us | 0.2184 us |   0.29 |
 |         MessagePack_TypeDeseralization | Core |    Core |  27.700 us | 0.2655 us | 0.2484 us |   0.17 |
 |    MessagePack_ProxyTypeDeseralization | Core |    Core |   4.632 us | 0.0186 us | 0.0174 us |   0.03 |
 |      MessagePackLz4_TypeDeseralization | Core |    Core |  28.309 us | 0.1192 us | 0.1115 us |   0.18 |
 | MessagePackLz4_ProxyTypeDeseralization | Core |    Core |   4.291 us | 0.0448 us | 0.0419 us |   0.03 |
```

Currently shows that
 * the https://www.nuget.org/packages/MessagePack library is the fastest
 * you dont have to pay a high cost for compression and decompression
 * only deserializing the properties you need can have a significant impact on performance
