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

|                             Method |       Mean |     Error |    StdDev |   Gen0 | Allocated |
|----------------------------------- |-----------:|----------:|----------:|-------:|----------:|
| ReverseString_StringbuilderForLoop |   807.3 ns |  13.72 ns |  15.80 ns | 0.0219 |    1.4 KB |
|          ReverseString_LinqReverse | 6,885.9 ns | 131.03 ns | 128.69 ns | 0.1221 |   7.76 KB |
|          ReverseString_SpanReverse | 2,368.0 ns |  45.98 ns |  68.82 ns | 0.0305 |   1.83 KB |
|          ReverseString_SpanForLoop | 2,623.1 ns |  46.87 ns |  48.14 ns | 0.0305 |   1.83 KB |

```
