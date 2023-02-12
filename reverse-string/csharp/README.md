# Reverse String
This question is asked by Google. Given a string, reverse all of its characters and return the resulting string.

Ex: Given the following strings...

```
“Cat”, return “taC”
“The Daily Byte”, return "etyB yliaD ehT”
“civic”, return “civic”
```

## Run the tests

```
cd revers-string\csharp
dotnet test
```

## Run Benchmark
```
cd reverse-string\csharp
dotnet run --project ./ReverseString.Benchmark/ReverseString.Benchmark.csproj -c Release

|                    Method |       Mean |     Error |    StdDev |     Median |   Gen0 | Allocated |
|-------------------------- |-----------:|----------:|----------:|-----------:|-------:|----------:|
| ReverseStringUsingForLoop |   796.8 ns |   7.33 ns |   6.50 ns |   797.5 ns | 0.0219 |    1.4 KB |
|    ReverseStringUsingLinq | 7,057.8 ns | 140.79 ns | 360.89 ns | 6,960.5 ns | 0.1221 |   7.76 KB |
|    ReverseStringUsingSpan | 2,461.6 ns |  49.15 ns | 136.18 ns | 2,404.3 ns | 0.0305 |   1.83 KB |

```
