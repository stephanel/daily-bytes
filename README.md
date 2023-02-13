# My Daily Bytes
My learning daily bytes using various tech/languages.

- [Reverse String - C#](https://github.com/stephanel/daily-bytes/tree/master/reverse-string/csharp)
- [Valid Palindrome - C#](https://github.com/stephanel/daily-bytes/tree/master/valid-palindrome/csharp)

## Run the .NET Tests using GitHub Actions

A workflow for running the .NET tests manually is available in `Github Actions`. When running it, You will be asked for the input `Folder of the .NET app` which should be the relative path of the directoy of the .NET code.

Example: to run the tests of the project `ReverseString`, enter `reverse-string/csharp`.
1. Click on `Actions` tab
2. Select `dotnet test` workflow
3. Expan the button `Run workflow`
4. Fill the textbox with the correct path, and click the button `Run workflow`

![run the 'dotnet test' workflow manually](./documentation/Manually%20run%20the%20'dotnet%20test'%20workflow.jpg)
