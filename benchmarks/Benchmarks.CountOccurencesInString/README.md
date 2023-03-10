``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.18363.1556/1909/November2019Update/19H2)
Intel Core i7-4750HQ CPU 2.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.102
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                                             Method | stringLength |         Mean |       Error |       StdDev | Ratio | RatioSD |    Gen0 |    Gen1 |    Gen2 | Allocated | Alloc Ratio |
|--------------------------------------------------- |------------- |-------------:|------------:|-------------:|------:|--------:|--------:|--------:|--------:|----------:|------------:|
|                   String.Count(c => c.Equals(search)) |         1000 |   8,405.8 ns |   152.79 ns |    366.08 ns |  1.00 |    0.00 |       - |       - |       - |      96 B |        1.00 *|
| **String.Length - String.Replace(search, &quot;).Length** |         **1000** |     **701.1 ns** |    **11.99 ns** |     **12.31 ns** |  **0.08** |    **0.00** |  **0.0296** |       **-** |       **-** |    **1984 B** |       **20.67** |
|                  String.Split(search).Length - 1 |         1000 |   1,576.2 ns |    30.98 ns |     31.81 ns |  0.19 |    0.01 |  0.0401 |       - |       - |    2688 B |       28.00 |
|                 **foreach(var c in String) count++** |         **1000** |     **924.7 ns** |     **8.01 ns** |      **7.10 ns** |  **0.11** |    **0.01** |       **-** |       **-** |       **-** |         **-** |        **0.00** |
|   foreach(var c in String.ToCharArray()) count++ |         1000 |   1,123.2 ns |    12.48 ns |     11.07 ns |  0.13 |    0.01 |  0.0305 |       - |       - |    2024 B |       21.08 |
|         **for (int i = 0; i &lt; length; i++) count++** |         **1000** |     **953.4 ns** |    **16.59 ns** |     **15.52 ns** |  **0.11** |    **0.01** |  **0.0305** |       **-** |       **-** |    **2024 B** |       **21.08** |
|    **for (int i = length - 1; i &gt;= 0; i--) count++** |         **1000** |     **936.6 ns** |     **7.24 ns** |      **6.42 ns** |  **0.11** |    **0.01** |  **0.0305** |       **-** |       **-** |    **2024 B** |       **21.08** |
|             while(String.indexOf() != 1) count++ |         1000 |   3,317.3 ns |    41.20 ns |     34.40 ns |  0.39 |    0.02 |       - |       - |       - |         - |        0.00 |
|                Regex(search).Matches(String).Count |         1000 |   4,422.1 ns |    82.02 ns |     72.71 ns |  0.51 |    0.03 |  0.0992 |       - |       - |    6648 B |       69.25 |
|  Regex.Match(String, Regex.Escape(search)).Count |         1000 |   3,875.3 ns |    66.19 ns |    114.17 ns |  0.46 |    0.02 |  0.0763 |       - |       - |    4824 B |       50.25 |
|       **foreach (var c in String.AsSpan()) count++** |         **1000** |     **841.9 ns** |     **6.88 ns** |      **6.09 ns** |  **0.10** |    **0.01** |       **-** |       **-** |       **-** |         **-** |        **0.00** |
|                                                    |              |              |             |              |       |         |         |         |         |           |             |
|                   String.Count(c => c.Equals(search)) |        10000 |  83,979.3 ns | 1,669.67 ns |  2,340.65 ns |  1.00 |    0.00 |       - |       - |       - |      96 B |        1.00 |
| **String.Length - String.Replace(search, &quot;).Length** |        **10000** |   **5,667.6 ns** |    **40.46 ns** |     **35.87 ns** |  **0.07** |    **0.00** |  **0.2975** |       **-** |       **-** |   **19720 B** |      **205.42** |
|                  String.Split(search).Length - 1 |        10000 |  14,403.5 ns |    80.24 ns |     62.65 ns |  0.17 |    0.01 |  0.4120 |  0.0305 |       - |   25280 B |      263.33 |
|                 **foreach(var c in String) count++** |        **10000** |   **9,807.0 ns** |   **186.85 ns** |    **183.51 ns** |  **0.12** |    **0.00** |       **-** |       **-** |       **-** |         **-** |        **0.00** |
|   foreach(var c in String.ToCharArray()) count++ |        10000 |  10,657.3 ns |    59.29 ns |     52.56 ns |  0.13 |    0.00 |  0.3052 |       - |       - |   20024 B |      208.58 |
|         **for (int i = 0; i &lt; length; i++) count++** |        **10000** |   **9,169.0 ns** |    **30.07 ns** |     **28.13 ns** |  **0.11** |    **0.00** |  **0.3052** |       **-** |       **-** |   **20024 B** |      **208.58** |
|    **for (int i = length - 1; i &gt;= 0; i--) count++** |        **10000** |   **9,145.7 ns** |    **43.28 ns** |     **40.48 ns** |  **0.11** |    **0.00** |  **0.3052** |       **-** |       **-** |   **20024 B** |      **208.58** |
|             while(String.indexOf() != 1) count++ |        10000 |  33,476.7 ns |   660.31 ns |    786.06 ns |  0.40 |    0.02 |       - |       - |       - |         - |        0.00 |
|                Regex(search).Matches(String).Count |        10000 |  30,886.7 ns |   592.91 ns |    495.11 ns |  0.37 |    0.01 |  0.6714 |  0.0610 |       - |   42544 B |      443.17 |
|  Regex.Match(String, Regex.Escape(search)).Count |        10000 |  29,741.4 ns |   545.41 ns |    483.50 ns |  0.35 |    0.01 |  0.6409 |  0.0610 |       - |   38640 B |      402.50 |
|       **foreach (var c in String.AsSpan()) count++** |        **10000** |   **9,012.0 ns** |   **136.00 ns** |    **127.22 ns** |  **0.11** |    **0.00** |       **-** |       **-** |       **-** |         **-** |        **0.00** |
|                                                    |              |              |             |              |       |         |         |         |         |           |             |
|               String.Count(c => c.Equals(search)) |       100000 | 823,062.5 ns | 2,360.12 ns |  2,092.19 ns |  1.00 |    0.00 |       - |       - |       - |      96 B |        1.00 |
| String.Length - String.Replace(search, &quot;).Length |       100000 | 165,196.1 ns |   590.39 ns |    523.37 ns |  0.20 |    0.00 | 62.2559 | 62.2559 | 62.2559 |  196826 B |    2,050.27 |
|                  String.Split(search).Length - 1 |       100000 | 181,440.4 ns | 2,100.80 ns |  1,965.09 ns |  0.22 |    0.00 |  5.3711 |  2.4414 |       - |  248332 B |    2,586.79 |
|                 **foreach(var c in String) count++** |       **100000** |  **89,064.6 ns** |   **407.92 ns** |    **340.63 ns** |  **0.11** |    **0.00** |       **-** |       **-** |       **-** |         **-** |        **0.00** |
|   foreach(var c in String.ToCharArray()) count++ |       100000 | 200,612.5 ns | 3,869.69 ns |  4,893.92 ns |  0.25 |    0.01 | 62.2559 | 62.2559 | 62.2559 |  200045 B |    2,083.80 |
|         for (int i = 0; i &lt; length; i++) count++ |       100000 | 178,892.0 ns | 1,595.07 ns |  1,245.33 ns |  0.22 |    0.00 | 62.2559 | 62.2559 | 62.2559 |  200045 B |    2,083.80 |
|    for (int i = length - 1; i &gt;= 0; i--) count++ |       100000 | 179,258.7 ns |   680.75 ns |    603.47 ns |  0.22 |    0.00 | 62.2559 | 62.2559 | 62.2559 |  200045 B |    2,083.80 |
|             while(String.indexOf() != 1) count++ |       100000 | 332,551.0 ns | 1,605.56 ns |  1,501.84 ns |  0.40 |    0.00 |       - |       - |       - |         - |        0.00 |
|                Regex(search).Matches(String).Count |       100000 | 298,676.8 ns | 5,818.80 ns |  7,146.01 ns |  0.36 |    0.01 |  6.8359 |  3.4180 |       - |  373928 B |    3,895.08 |
|  Regex.Match(String, Regex.Escape(search)).Count |       100000 | 297,268.7 ns | 5,901.47 ns | 10,938.76 ns |  0.37 |    0.02 |  5.8594 |  2.4414 |       - |  363368 B |    3,785.08 |
|       **foreach (var c in String.AsSpan()) count++** |       **100000** |  **87,839.5 ns** |   **353.88 ns** |    **331.02 ns** |  **0.11** |    **0.00** |       **-** |       **-** |       **-** |         **-** |        **0.00** |
