```

BenchmarkDotNet v0.13.12, macOS Sonoma 14.5 (23F79) [Darwin 23.5.0]
Apple M2 Max, 1 CPU, 12 logical and 12 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.6 (8.0.624.26715), Arm64 RyuJIT AdvSIMD


```
| Method                | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Gen2   | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|----------:|------:|--------:|-------:|-------:|-------:|----------:|------------:|
| DefaultBindings       |  2.493 μs | 0.0142 μs | 0.0126 μs |  1.00 |    0.00 | 0.2708 | 0.1335 |      - |   2.22 KB |        1.00 |
| DefaultBindingsMarkup |  2.494 μs | 0.0101 μs | 0.0085 μs |  1.00 |    0.01 | 0.2708 | 0.1335 |      - |   2.22 KB |        1.00 |
| TypedBindingsMarkup   | 32.235 μs | 0.3598 μs | 0.3366 μs | 12.95 |    0.15 | 1.3428 | 0.6714 | 0.0610 |  11.13 KB |        5.02 |
