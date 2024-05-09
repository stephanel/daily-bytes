# My Daily Bytes

My learning daily bytes using various tech/languages.

## Learnings

- [Aspire Application - C#](./learning/aspire)
- [Castle Dynamic Proxy - C#](./learning/castle-dynamic-proxy)
- [Code Instrumentation using OpenTelemetry - C#](./learning/opentelemetry)

## Design Patterns

- [Chain of Responsibility - C#](./patterns/chain-of-responsibility/csharp/)
- [Railway Oriented Programming - C#/F#](./patterns/railway-oriented-programming/dotnet)
- [Synchronous/Asynchronous Modules Communication - C#/Rx.Net](./patterns/modules-communication/csharp/)

## Code Kata

- [Reverse String - C#](./kata/reverse-string/csharp)
- [Valid Palindrome - C#](./kata/valid-palindrome/csharp)
- [Vacuum Cleaner Route - C#](./kata/vaccum-cleaner-route/csharp)
- [Correct Capitalization - C#](./kata/correct-capitalization/csharp)

## Benchmarks

- [Count number of occurrences in string - C#](./benchmarks/Benchmarks.CountOccurencesInString)
- [Loop through collections - C#](./benchmarks/Benchmarks.ForEachLoops)

## Run the .NET Tests using GitHub Actions

A workflow for running the .NET tests manually is available in `Github Actions`. When running it, You will be asked for the input `Folder of the .NET app` which should be the relative path of the directory of the .NET code.

Example: to run the tests of the project `ReverseString`, enter `reverse-string/csharp`.

1. Click on `Actions` tab
2. Select `dotnet test` workflow
3. Expand the button `Run workflow`
4. Fill the textbox with the correct path, and click the button `Run workflow`

![run the 'dotnet test' workflow manually](./documentation/Manually%20run%20the%20'dotnet%20test'%20workflow.jpg)

## Add a new C# project

```bash
mkdir newdir
cd newdir

dotnet new sln -n solution_name
dotnet new console -o /path/to/project/folder -n project_name
dotnet sln add /path/to/project/folder
dotnet add /path/to/project/folder package package_name
```
