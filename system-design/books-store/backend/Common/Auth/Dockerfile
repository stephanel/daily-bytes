FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy source files
#/App
#   |-Common
#   |   |-Auth
#   |   |-Common.Extensions
#   |   |-Common.Extensions.API
#   |   |-Common.Infrastructure
#   |-Directory.Build.props
#   |-Directory.Packages.props

COPY ./Common/Auth                  ./Common/Auth
COPY ./Common/Common.Extensions     ./Common/Common.Extensions
COPY ./Common/Common.Extensions.API ./Common/Common.Extensions.API
COPY ./Common/Common.Infrastructure ./Common/Common.Infrastructure
COPY ./Directory.Build.props        ./Directory.Build.props
COPY ./Directory.Packages.props     ./Directory.Packages.props

# Restore as distinct layers
RUN dotnet restore ./Common/Auth/Auth.csproj

# Build and publish a release
RUN dotnet publish -c Release -o out ./Common/Auth/Auth.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Auth.WebApi.dll"]