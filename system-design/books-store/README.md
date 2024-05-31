Books Store - Personal project to put new technologies or models into practice.

- [Technologies](#technologies)
- [Features](#features)
- [Architecture](#architecture)
- [Folder Structure](#folder-structure)
- [Projects and Solutions](#projects-and-solutions)
  - [Common Projects](#common-projects)
  - [Backend - Microservices](#backend---microservices)
    - [Source Projects](#source-projects)
    - [Test Projects](#test-projects)
  - [Backend - Monolith](#backend---monolith)
  - [Frontend](#frontend)
  - [Testing Strategy](#testing-strategy)
    - [Unit Tests](#unit-tests)
    - [Integration Tests](#integration-tests)
- [Local Setup](#local-setup)
  - [OpenTelemetry Configuration](#opentelemetry-configuration)
  - [Verify](#verify)
  - [Add a new database migration](#add-a-new-database-migration)
  - [Update the database](#update-the-database)
- [Run applications](#run-applications)
  - [Start the required services](#start-the-required-services)
  - [Environment Variables](#environment-variables)
  - [Application Settings](#application-settings)
  - [Run Applications](#run-applications-1)
    - [Backend](#backend)
    - [Frontend](#frontend-1)
- [Learning Resources](#learning-resources)

## Technologies

- Backend
  - [.NET 8](https://dotnet.microsoft.com/en-us/)
  - [YARP](https://microsoft.github.io/reverse-proxy/)
  - [FastEndpoints](https://fast-endpoints.com/)
  - [Mediator](https://github.com/martinothamar/Mediator)
  - [MassTransit](https://github.com/MassTransit/MassTransit)
  - [Serilog](https://github.com/serilog/serilog)
  - [OpenTelemetry](https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation)
  - [Xunit](https://xunit.net/), [FluentAssertions](https://github.com/fluentassertions/fluentassertions), [Verify](https://github.com/VerifyTests/Verify), [NetArchTest](https://github.com/BenMorris/NetArchTest)
- Frontend
  - [Angular](https://angular.io/docs)
  - [React](https://react.dev/learn)
  - [Vue.js](https://vuejs.org/guide/introduction.html)
- Infrastructure
  - Docker
  - PostgreSQL
  - TBD - RabbitMQ, Kafka
  - Grafana, Loki (logs), Prometheus (metrics), Tempo (traces), Aspire
- DevOps
  - TBD - CI/CD
  - TBD - Quality Gate
- Deployment - TBD
- Performances
  - [k6](https://k6.io/)

## Features

- As a user, I can view the catalog of books
- As a user, I can search for books
- As a user, I can view the details of a book
- As a user, I can order books: 3 books limit per order
- As an unregistered user, I can order books with the same limitation as a registered user 
- As a user, I can create a free user account
- As a user, I can subscribe to the Premium offer, which increases the books limit to 5 per order, and gives access to promotions
- As a user, I can cancel my subscription at any moment
- As a registered user, I can view and edit my contact info
- As a registered user, I can view the status my his subscription
- As a registered user, I can view my orders history
- make UI multilingual?

## Architecture

See [System Design Documentation](./docs/system-design.md)

## Folder Structure

- [Docker Compose Files](./.docker)
- [Backend](./backend)
  - [Common](./backend/Common) 
  - [Microservices Architecture](./backend/microservices)
  - [Monolithic Architecture](./backend/monolithic) - Upcoming
- [Frontend](./frontend)
  - [Angular](./frontend/angular) - In Progress
  - [React](./frontend/react) - In Progress
  - [Vue.js](./frontend/vuejs) - In Progress

## Projects and Solutions

### Common Projects

The following projects are common to both microservices and monolithic implementation.

Source folder: [./backend/common/](./backend/common/)

| Project | Folder | Ports (http, https) | Database | API Gateway Config |
| - | - | - | - | - |
| API.Gateway | Common/ApiGateway/ | 5217, 5218 | | |
| Auth | Common/Auth/ | 5219, 5220 | identity | ✅ |
| Common.Extensions | Common/Common.Extensions/ | - | | |
| Common.Extensions.API | Common/Common.Extensions.API/ | - | | |
| Common.Infrastructure | Common/Common.Infrastructure/ | - | | |
| Common.TestFramework | Common.TestFramework/ | - | | |

### Backend - Microservices


#### Source Projects

Source folder: [./backend/microservices/src/](./backend/microservices/src/)

Each projects is composed of 4 layers: API (or Consumer), Application, Domain, Infrastructure. 

| Service | Project | Type | Ports (http, https) | Database | API Gateway Config |
| - | - | - | - | - | - |
| Books API | Books.[layer] | Web API | 5270, 5271 | books | ✅ |
| OrdersHistory API | OrdersHistory.[layer] | Web API | 5272, 5273 | | |
| OrderManagement Service | OrderManagement.[layer] | Web App (Consumer) | 5274,5275  | | |
| UserAccounts API | UserAccounts.[layer] | Web API | 5276, 5277 | | |
| Subscriptions API | Subscriptions.[layer] | Web API | 5278, 5279 | | |


#### Test Projects

See section [Testing Strategy](#testing-strategy) for more details.

Tests folder:  [./backend/microservices/tests/](./backend/microservices/tests/)

Each service has an integration tests project, and a unit tests project. 

| Service | Project |
| ------------- | ------------- |
| Books API | Books.API.IntegrationTests<br />Books.UnitTests |
| OrdersHistory API | OrdersHistory.API.IntegrationTests<br />OrdersHistory.UnitTests |
| UserAccounts API | UserAccounts.API.IntegrationTests<br />UserAccounts.UnitTests |
| Subscriptions API | Subscriptions.API.IntegrationTests<br />Subscriptions.UnitTests |
| OrderManagement Service | OrderManagement.Service.IntegrationTests<br />OrderManagement.UnitTests |

FIXME: :warning: Merge integration tests projects into a single one

### Backend - Monolith

TBA

### Frontend

Source folder:  [./frontend](./frontend/)

3 projects were created to build the same using 3 different framework: Angular, React, Vue.js. The tests are stored with the source code files as is the convention in frontend world. 

| Project/Folder |
| ------------- |
| [angular](./frontend/angular/) |
| [react](./frontend/react/) |
| [vuejs](./frontend/vuejs/) |


### Testing Strategy

#### Unit Tests

Each project should have its own unit tests project.

#### Integration Tests

Each service should have its own integration tests project.

📝:warning: Consider having a single integration tests project where the tests would call through the API Gateway. It would cover both backend implementation, microservices, and modular monolith. 



## Local Setup

### OpenTelemetry Configuration

- [Get Started with Prometheus and Grafana](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/docs/metrics/getting-started-prometheus-grafana/README.md)
- [OpenTelemetry.Extensions.Hosting Usage](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Extensions.Hosting/README.md)
- Configure instrumentation:
  - [AspNetCore](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.AspNetCore)
  - [HttpClient](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.Http)
  - [EFCore](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/blob/main/src/OpenTelemetry.Instrumentation.EntityFrameworkCore/README.md)
  - [Npgsql](https://www.npgsql.org/doc/diagnostics/tracing.html)
  - [MassTransit](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.MassTransit)
- Configure exporters:
  - [Console Exporter](https://github.com/open-telemetry/opentelemetry-dotnet/tree/main/src/OpenTelemetry.Exporter.Console) 
  - [Prometheus Exporter](https://github.com/open-telemetry/opentelemetry-dotnet/tree/main/src/OpenTelemetry.Exporter.Prometheus.AspNetCore)
  - [Serilog.Sinks.OpenTelemetry](https://github.com/serilog/serilog-sinks-opentelemetry)

### Verify

Check [Verify GitHub Repository](https://github.com/VerifyTests/Verify?tab=readme-ov-file) and [Getting started wizard](https://github.com/VerifyTests/Verify/blob/main/docs/wiz/readme.md) for local setup.

:warning: Windows setup may require  [Windows Long Paths Support](https://learn.microsoft.com/en-us/windows/win32/fileio/maximum-file-path-limitation?tabs=powershell) enabled since `Verify` can generates long file names (see [`Verify`  section](#verify) below).



### Add a new database migration

Before using migrations add the dotnet ef core tools

```bash
dotnet tool install --global dotnet-ef
```

Run the following command replacing MyMigration with your migration name:
```bash
cd ./system-design/books-store/backend/microservices/src

dotnet ef migrations add Create_Initial -p Books.Infrastructure -s Books.API -c BooksDbContext -o Persistence/Migrations -v
```

### Update the database


Run the following to update your local database:
```bash
cd ./books-store/backend/microservices/src

# Books API
dotnet ef database update -p Books.Infrastructure -s Books.API -c BooksDbContext -v
```

## Run applications

### Start the required services

Start the required resources by running the following commands:

```bash
cd .docker
docker compose -f postgres-docker-compose.yml -f rabbitmq-docker-compose.yml -f observability-docker-compose.yml up

# 📝 replace `observability-docker-compose.yml` 
# by `aspire-dashboard-docker-compose.yml`
# to use Aspire Dashboard instead of Grafana
```
Then navigate to:
- [RabbitMQ Management UI](http://localhost:15672)
- [Grafana](http://localhost:3000), or [Aspire Dashboard](http://localhost:18888/)
- [Prometheus](http://localhost:8080/)
- Books API - [Swagger UI](https://localhost:7141/swagger/index.html) - [OAS](https://localhost:7141/swagger/v1/swagger.json)

:warning: When using `observability-docker-compose.yml` the following folder structure needs to be created manually for the docker containers to start properly the first time.

```
- books-store
  |--.docker
      |--docker_volumes
          |--prometheus
              |--data
```

### Environment Variables

- `OTEL_EXPORTER_OTPL_ENDPOINT`:  endpoint of the OpenTelemetry collector (default value: http://localhost:4317)
- `OTEL_SERVICE_NAME`: name of the application that sends the collected data. :warning: FIXME: should it be moved to application settings?

### Application Settings

- `ConnectionStrings:BooksDatabaseConnectionString`: use the database, the users and the password respectively assigned to the environment variables `POSTGRES_MULTIPLE_DATABASES` and `POSTGRES_PASSWORD` in docker compose file `.docker/postgres-docker-compose.yml`.


### Run Applications

#### Backend
TBA

#### Frontend

Navigate to the folder of the the frontend project you would like to run (angular, react or vuejs) and run the following commands:

```bash
npm install # or yarn install
npx nx serve
```
Then navigate to:
- Angular: http://localhost:4200
- React: http://localhost:4201
- Vue.js: http://localhost:4202


## Learning Resources

- [C4 Models](https://c4model.com/)
- [CQRS](https://martinfowler.com/bliki/CQRS.html)
- [API Gateway using YARP](https://microsoft.github.io/reverse-proxy/articles/getting-started.html)
- .NET 8 AspNetCore Identity
  - [Overview](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio)
  - [The MapIdentityApi<TUser> endpoints](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0#the-mapidentityapituser-endpoints)
  - [Token Base Authentication](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/standalone-with-identity?view=aspnetcore-8.0#token-authentication)
  - [Secure a Web API backend for SPAs](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0)
- [Angular](https://angular.io/docs)
- [React](https://react.dev/learn)
- [Vue.js](https://vuejs.org/guide/introduction.html)
