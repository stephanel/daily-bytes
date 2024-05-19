# Books Store

Personal project to put new technologies or models into practice.

## Content

- [Books Store](#books-store)
  - [Content](#content)
  - [Folder Structure](#folder-structure)
  - [Technologies](#technologies)
  - [Features](#features)
  - [Architecture](#architecture)
  - [Solutions](#solutions)
    - [Common Projects](#common-projects)
    - [Backend - Microservices](#backend---microservices)
      - [Source Projects](#source-projects)
      - [Test Projects](#test-projects)
    - [Backend - Monolith](#backend---monolith)
    - [Frontend - Angular](#frontend---angular)
    - [Frontend - React - Upcoming!](#frontend---react---upcoming)
    - [Frontend - Vue.js - Upcoming!](#frontend---vuejs---upcoming)
  - [Local Setup](#local-setup)
    - [OpenTelemetry Configuration](#opentelemetry-configuration)
    - [Verify](#verify)
    - [Add a new database migration](#add-a-new-database-migration)
    - [Update the database](#update-the-database)
  - [Run applications](#run-applications)
    - [Start the required services](#start-the-required-services)
  - [Learning Resources](#learning-resources)

## Folder Structure

- [Docker Compose Files](./.docker)
- [Backend](./backend)
  - [Microservices Architecture](./backend/microservices)
  - [Monolithic Architecture](./backend/monolithic) - Upcoming
- [Frontend](./frontend) - Upcoming
  - [Angular](./frontend/angular) - Upcoming
  - [React](./frontend/react) - Upcoming
  - [Vue.js](./frontend/vuejs) - Upcoming

## Technologies

- Backend
  - [.NET](https://dotnet.microsoft.com/en-us/)
  - [FastEndpoints](https://fast-endpoints.com/)
  - [Mediator](https://github.com/martinothamar/Mediator)
  - [MassTransit](https://github.com/MassTransit/MassTransit)
  - [Serilog](https://github.com/serilog/serilog)
  - [OpenTelemetry .Net Instrumentation](https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation)
  - [Xunit](https://xunit.net/)
  - [Verify](https://github.com/VerifyTests/Verify)
- Frontend
  - [Angular](https://angular.io/)
  - React - Upcoming!
  - Vue.js - Upcoming!
- Infrastructure
  - PostgreSQL
  - TBD - RabbitMQ, Kafka
  - Grafana, Loki (logs), Prometheus (metrics), Aspire
- DevOps
  - TBD - CI/CD Pipelines
  - TBD - Quality Gate
- Performances
  - [k6](https://k6.io/)

## Features

- The user can view the catalog of books
- The user can search for books
- The user can view the details of a book
- The user can order books: 3 books limit per order
- The user can create a free user account
- The user can subscribe to Premium offer
  - Increases the books limit per order to 5 books
  - Gives access to promotions
- The user can cancel his subscription
- The user can view and edit his contact info
- The user can view the status of his subscription
- The user can view his orders history
- make UI multilingual?

## Architecture

See [System Design Documentation](./docs/system-design.md)

## Solutions

### Common Projects

The following projects are common to both microservices and monolithic implementation.

Source folder: `backend/common/`

| Project | Folder |
| ------------- | ------------- |
| ApiGateway | Common/ApiGateway/ |
| Common.Extensions | Common/CommonExtensions/ |
| Common.Extensions.API | Common/CommonExtensions.API/ |
| Common.TestFramework | Common.TestFramework/ |

### Backend - Microservices


#### Source Projects

Source folder: `backend/microservices/src/`

Each projects is composed of 4 layers: API (or Consumer), Application, Domain, Infrastructure. 

| Service | Project | Type |
| ------------- | ------------- | ------------- |
| Books API | Books.[layer] | Web API |
| OrdersHistory API | OrdersHistory.[layer] | Web API |
| UserAccounts API | UserAccounts.[layer] | Web API |
| Subscriptions API | Subscriptions.[layer] | Web API |
| OrderManagement Service | OrderManagement.[layer] | Web App (Consumer) |


#### Test Projects

Tests folder:  `backend/microservices/tests/`

Each service has an integration tests project, and a unit tests project. 

| Service | Project |
| ------------- | ------------- |
| Books API | Books.API.IntegrationTests<br />Books.UnitTests |
| OrdersHistory API | OrdersHistory.API.IntegrationTests<br />OrdersHistory.UnitTests |
| UserAccounts API | UserAccounts.API.IntegrationTests<br />UserAccounts.UnitTests |
| Subscriptions API | Subscriptions.API.IntegrationTests<br />Subscriptions.UnitTests |
| OrderManagement Service | OrderManagement.Service.IntegrationTests<br />OrderManagement.UnitTests |

FIXME: :warning: Should we merge integration tests projects into a single one?

### Backend - Monolith

TBA

### Frontend - Angular
TBA

### Frontend - React - Upcoming!
Upcoming

### Frontend - Vue.js - Upcoming!
Upcoming

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

## Learning Resources

- [C4 Models](https://c4model.com/)
- [CQRS](https://martinfowler.com/bliki/CQRS.html)
