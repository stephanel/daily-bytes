# My Daily Bytes
My learning daily bytes using various tech/languages.

## Code Kata
- [Reverse String - C#](https://github.com/stephanel/daily-bytes/tree/master/reverse-string/csharp)
- [Valid Palindrome - C#](https://github.com/stephanel/daily-bytes/tree/master/valid-palindrome/csharp)
- [Vaccum Cleaner Route - C#](https://github.com/stephanel/daily-bytes/tree/master/vaccum-cleaner-route/csharp)
- [Correct Capitalization - C#](https://github.com/stephanel/daily-bytes/tree/master/correct-capitalization/csharp)

## Benchmarks
- [Count number of occurences in string - C#](https://github.com/stephanel/daily-bytes/tree/master/benchmarks/Benchmarks.CountOccurencesInString)

## OpenTelemetry Code Instrumentation
- [OpenTelemetry AspNetCore Intrumentation](https://github.com/stephanel/daily-bytes/tree/master/opentelemetry/csharp)

## Run the .NET Tests using GitHub Actions

A workflow for running the .NET tests manually is available in `Github Actions`. When running it, You will be asked for the input `Folder of the .NET app` which should be the relative path of the directoy of the .NET code.

Example: to run the tests of the project `ReverseString`, enter `reverse-string/csharp`.
1. Click on `Actions` tab
2. Select `dotnet test` workflow
3. Expan the button `Run workflow`
4. Fill the textbox with the correct path, and click the button `Run workflow`

![run the 'dotnet test' workflow manually](./documentation/Manually%20run%20the%20'dotnet%20test'%20workflow.jpg)

## Add a new C# project
```
mkdir newdir
cd newdir

dotnet new sln -n solution_name
dotnet new console -o /path/to/project/folder -n project_name
dotnet sln add /path/to/project/folder
dotnet add /path/to/project/folder package package_name
```
