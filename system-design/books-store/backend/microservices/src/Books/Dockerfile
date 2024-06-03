FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy source files
#/App
#   |-Common
#   |   |-Common.Extensions
#   |   |-Common.Extensions.API
#   |   |-Common.Infrastructure
#   |-microservices
#   |   |-src
#   |       |-Books
#   |           |-Books.API
#   |           |-Books.Application
#   |           |-Books.Domain
#   |           |-Books.Infrastructure
#   |-Directory.Build.props
#   |-Directory.Packages.props

COPY ./Common/Common.Extensions     ./Common/Common.Extensions
COPY ./Common/Common.Extensions.API ./Common/Common.Extensions.API
COPY ./Common/Common.Infrastructure ./Common/Common.Infrastructure
COPY ./microservices/src/Books      ./microservices/src/Books
COPY ./Directory.Build.props        ./Directory.Build.props
COPY ./Directory.Packages.props     ./Directory.Packages.props

# Restore as distinct layers
RUN dotnet restore ./microservices/src/Books/Books.API/Books.API.csproj

# Build and publish a release
RUN dotnet publish -c Release -o out ./microservices/src/Books/Books.API/Books.API.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 9020
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Books.API.dll"]