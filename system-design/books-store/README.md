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
  - [Start applications](#start-applications)
    - [Start the required services](#start-the-required-services)
      - [How to add a new migration](#how-to-add-a-new-migration)
    - [Update the database](#update-the-database)
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
  - [MassTransit](https://github.com/MassTransit/MassTransit)
  - [Mediator](https://github.com/martinothamar/Mediator)
  - [OpenTelemetry](https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation)
  - [Serilog](https://github.com/serilog/serilog)
- Frontend
  - [Angular](https://angular.io/)
  - React - Upcoming!
  - Vue.js - Upcoming!
- Infrastructure
  - PostgreSQL
  - TBD - RabbitMQ, Kafka
  - TBD - Grafana, Aspire, Jaeger/Seq
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

## Start applications

### Start the required services

Start the required resources by running the following commands:

```bash
cd .docker
docker compose -f postgres-docker-compose.yml -f rabbitmq-docker-compose.yml -f grafana-docker-compose.yml up
```
Then navigate to:
- [RabbitMQ Management UI](http://localhost:15672)
- [Grafana](http://localhost:3000)
- Books API - [Swagger UI](https://localhost:7141/swagger/index.html) - [OAS](https://localhost:7141/swagger/v1/swagger.json)


#### How to add a new migration

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


## Learning Resources

- [C4 Models](https://c4model.com/)
- [CQRS](https://martinfowler.com/bliki/CQRS.html)
