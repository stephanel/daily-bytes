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
cd revers-string\csharp
dotnet run --project ./ReverseString.Benchmark/ReverseString.Benchmark.csproj -c Release

|                    Method |       Mean |     Error |    StdDev |     Median |   Gen0 | Allocated |
|-------------------------- |-----------:|----------:|----------:|-----------:|-------:|----------:|
| ReverseStringUsingForLoop |   839.6 ns |  16.69 ns |  46.24 ns |   829.8 ns | 0.0219 |    1.4 KB |
|    ReverseStringUsingLinq | 7,067.0 ns | 213.58 ns | 591.82 ns | 6,801.7 ns | 0.1221 |   7.76 KB |

```
