```

BenchmarkDotNet v0.13.12, macOS Sonoma 14.5 (23F79) [Darwin 23.5.0]
Apple M2 Max, 1 CPU, 12 logical and 12 physical cores
.NET SDK 8.0.302
  [Host] : .NET 8.0.6 (8.0.624.26715), Arm64 RyuJIT AdvSIMD


```
| Method                | Mean | Error | Ratio | RatioSD | Alloc Ratio |
|---------------------- |-----:|------:|------:|--------:|------------:|
| DefaultBindings       |   NA |    NA |     ? |       ? |           ? |
| DefaultBindingsMarkup |   NA |    NA |     ? |       ? |           ? |
| TypedBindingsMarkup   |   NA |    NA |     ? |       ? |           ? |

Benchmarks with issues:
  ExecuteBindings.DefaultBindings: DefaultJob
  ExecuteBindings.DefaultBindingsMarkup: DefaultJob
  ExecuteBindings.TypedBindingsMarkup: DefaultJob
