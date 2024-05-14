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
    - [Backend - Microservices](#backend---microservices)
    - [Backend - Monolith](#backend---monolith)
  - [Start applications](#start-applications)
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

### Backend - Microservices

- Root folder - `backend`: backend/
- Microservices source folder - `src`: {backend}/microservices/src/
- Microservices tests folder - `tests`: {backend}/microservices/tests/


| Project | Assembly | Folder |
| ------------- | ------------- | ------------- |
| ApiGateway | BookStore.ApiGateway | {backend}/ApiGateway |
| Common.Extensions | BookStore.Common.Extensions | {backend}/Common/CommonExtensions |
| Books.API | BookStore.Books.API | {src}/Books.API |
| OrdersHistory.API | BookStore.OrdersHistory.API | {src}/OrdersHistory.API |
| UserAccounts.API | BookStore.UserAccounts.API | {src}/UserAccounts.API |
| Subscriptions.API | BookStore.Subscriptions.API | {src}/Subscriptions.API |
| OrderManagement.Service | BookStore.OrderManagement.Service | {src}/OrderManagement.Service |
| Books.API.IntegrationTests | BookStore.Books.API.IntegrationTests | {tests}/Books.API.IntegrationTests |
| OrdersHistory.API.IntegrationTests | BookStore.OrdersHistory.API.IntegrationTests | {tests}/OrdersHistory.API.IntegrationTests |
| UserAccounts.API.IntegrationTests | BookStore.UserAccounts.API.IntegrationTests | {tests}/UserAccounts.API.IntegrationTests |
| Subscriptions.API.IntegrationTests | BookStore.Subscriptions.API.IntegrationTests | {tests}/Subscriptions.API.IntegrationTests |
| OrderManagement.Service.IntegrationTests | BookStore.OrderManagement.Service.IntegrationTests | {tests}/OrderManagement.Service.IntegrationTests |
| Books.UnitTests | BookStore.Books.UnitTests | {tests}/Books.UnitTests |

### Backend - Monolith

TBA

## Start applications

Start the required resources by running the following commands:

```bash
cd .docker
docker compose -f postgres-docker-compose.yml -f rabbitmq-docker-compose.yml -f grafana-docker-compose.yml up
```
Then navigate to:
- [RabbitMQ Management UI](http://localhost:15672)
- [Grafana](http://localhost:3000)
- Books API - [Swagger UI](https://localhost:7141/swagger/index.html) - [OAS](https://localhost:7141/swagger/v1/swagger.json)

## Learning Resources

- [C4 Models](https://c4model.com/)
- [CQRS](https://martinfowler.com/bliki/CQRS.html)
