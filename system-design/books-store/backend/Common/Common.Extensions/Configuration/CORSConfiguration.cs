namespace Common.Extensions.Configuration;

#pragma warning disable S101 // Types should be named in PascalCase
public record CORSConfiguration(string PolicyName, string[] Origins);
#pragma warning restore S101 // Types should be named in PascalCase