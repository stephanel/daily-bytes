FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy source files
#/App
#   |-Common
#   |   |-ApiGateway
#   |   |-Common.Extensions
#   |   |-Common.Extensions.API
#   |   |-Common.Infrastructure
#   |-Directory.Build.props
#   |-Directory.Packages.props

COPY ./Common/ApiGateway            ./Common/ApiGateway
COPY ./Common/Common.Extensions     ./Common/Common.Extensions
COPY ./Common/Common.Extensions.API ./Common/Common.Extensions.API
COPY ./Common/Common.Infrastructure ./Common/Common.Infrastructure
COPY ./Directory.Build.props        ./Directory.Build.props
COPY ./Directory.Packages.props     ./Directory.Packages.props

# Restore as distinct layers
RUN dotnet restore ./Common/ApiGateway/API.Gateway.csproj

# Build and publish a release
RUN dotnet publish -c Release -o out ./Common/ApiGateway/API.Gateway.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "API.Gateway.WebApi.dll"]