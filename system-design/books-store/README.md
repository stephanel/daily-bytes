# Books Store

Personal project to put new technologies or models into practice.

## Content

- [Folder Structure](#folder-structure)
- [Technologies](#technologies)
- [Architecture](#architecture)
  - [Microservices](#microservices)
    - [C4 System Context Diagram](#c4-system-context-diagram)
    - [C4 Container Diagram](#c4-container-diagram)
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

### Microservices

#### C4 System Context Diagram

```mermaid
    C4Context
      title System Context diagram for Books Store System

      %% layout     
      UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")

      %% users
      Person(customerA, "Books Store Customer A", "A customer of the books store system,<br /> with a personal account.")
      Person(customerB, "Books Store Customer B", "A customer of the books store system,<br /> with a personal account.")
      Person_Ext(customerC, "Books Store Customer C", "A customer of the books store service,<br /> without a personal account")

      %% systems
      System(EmailSystem, "Email System", "Send emails")
      System(BooksStoreSystem, "Books Store System", "Allows customers to view the books catalog, or order books.")

      %% relationships
      Rel(customerA, BooksStoreSystem, "View and order books,<br /> and view user account")
      Rel(customerB, BooksStoreSystem, "View and order books,<br /> and view user account")
      Rel(customerC, BooksStoreSystem, "View and<br /> order books")
      Rel(BooksStoreSystem, EmailSystem, "Sends e-mails<br /> using", "SMTP")
      Rel(EmailSystem, customerA, "Sends e-mails to")

      %% styles
      UpdateElementStyle(EmailSystem, $bgColor="grey", $borderColor="red")
      UpdateRelStyle(customerA, BooksStoreSystem, $textColor="blue", $lineColor="blue", $offsetX="-50")
      UpdateRelStyle(customerB, BooksStoreSystem, $textColor="blue", $lineColor="blue", $offsetX="-50")
      UpdateRelStyle(customerC, BooksStoreSystem, $textColor="blue", $lineColor="blue", $offsetX="-25")
      UpdateRelStyle(BooksStoreSystem, EmailSystem, $textColor="blue", $lineColor="blue", $offsetY="-10", $offsetX="-30")
      UpdateRelStyle(EmailSystem, customerA, $textColor="red", $lineColor="red", $offsetX="-50", $offsetY="20")
```

#### C4 Container Diagram

```mermaid
    C4Container
      title System Context diagram for Books Store System

      %% layout     
      UpdateLayoutConfig($c4ShapeInRow="2", $c4BoundaryInRow="1")

      %% users
      Person(customerA, "Books Store Customer A", "A customer of the books store system,<br /> with a personal account.")
      Person_Ext(customerC, "Books Store Customer C", "A customer of the books store service,<br /> without a personal account")
      System(EmailSystem, "Email System", "Send emails")
      System(OTELCollector, "OTEL Collector", "Collector logs, traces and metrics")

      %% systems

 Container_Boundary(booksStore, "Books Store") {
        Container(spa, "Single-Page App", "JavaScript, Angular/React/Vue.js", "Provides all the functionality to customers via their web browser")
        Container(api_gateway, "API Gateway", ".NET, Docker Container", "Provides a secured single point of entry<br /> to the books store system.")
        Container(books_api, "Books API", ".NET, Docker Container", "Delivers the books details and catalog")
        Container(users_api, "User Accounts API", ".NET, Docker Container", "Delivers the user accounts details")
        Container(orders_history_api, "Orders History API", ".NET, Docker Container", "Delivers the customers' orders history")
        Container(subscriptions_api, "Subscription API", ".NET, Docker Container", "Delivers the customers' orders history")

        ContainerDb(books_database, "Database", "SQL Database", "Stores user registration information, hashed auth credentials, access logs, etc.")

        Container(message_broker, "Message Broker", "RabbitMQ/Kafka, Docker Container", "???")

        Container(order_management, "Order Management", ".NET, Docker Container", "Processes customer orders")


    }
      %% relationships
      Rel(customerA, spa, "View and order books,<br /> and view user account")
      Rel(customerC, spa, "View and<br /> order books")
      Rel(spa, api_gateway, "Securely calls backend functionalities", "HTTP")
      Rel(api_gateway, books_api, "Routes calls to invoked API", "HTTP")
      Rel(books_api, books_database, "Read from, write to", "SQL")
      
      Rel(api_gateway, users_api, "Routes calls to invoked API", "HTTP")
      Rel(users_api, books_database, "Read from, write to", "SQL")
      Rel(api_gateway, orders_history_api, "Routes calls to invoked API", "HTTP")
      Rel(orders_history_api, books_database, "Read from, write to", "SQL")

      Rel(books_api, message_broker, "publishes to", "RabbitMQ/Kafka")

      Rel(order_management, message_broker, "consumes from", "RabbitMQ/Kafka")
      Rel(order_management, books_database, "Read from, write to", "SQL")
      
      Rel(EmailSystem, customerA, "Sends e-mails to")

      %% styles
  
      UpdateElementStyle(EmailSystem, $bgColor="grey", $borderColor="white")
      UpdateElementStyle(OTELCollector, $bgColor="grey", $borderColor="white")
      UpdateRelStyle(customerA, BooksStoreSystem, $textColor="blue", $lineColor="blue", $offsetX="-50")
      UpdateRelStyle(customerC, BooksStoreSystem, $textColor="blue", $lineColor="blue", $offsetX="-25")
      UpdateRelStyle(BooksStoreSystem, EmailSystem, $textColor="blue", $lineColor="blue", $offsetY="-10", $offsetX="-30")
      UpdateRelStyle(EmailSystem, customerA, $textColor="red", $lineColor="red", $offsetX="-50", $offsetY="20")
```

```mermaid
graph TB;
    classDef internal_component fill:#5b65ff,color:white,stroke:lightgrey,stroke-width:2px
    classDef external_system fill:grey,color:white,stroke:lightgrey,stroke-width:2px

    user[Web Browser]

    subgraph Books Store System
      spa[Single Page Application]:::internal_component
      api_gateway[API Gateway]:::internal_component
      subgraph apis [APIs]
        books_api[Books API]:::internal_component
        orders_api[Orders API]:::internal_component
        users_api[User Accounts API]:::internal_component
        subscriptions_api[Subscription API]:::internal_component
        orders_history_api[Orders History API]:::internal_component
      end
      subgraph databases [Databases]
        books_database[(Books Database)]:::internal_component
        orders_database[(Orders Database)]:::internal_component
        users_database[(Users Database)]:::internal_component
        subscription_database[(Subscription Database)]:::internal_component
        orders_history_database[(Order History Database)]:::internal_component
      end

      subgraph services [Services]
        orders_management_service[Orders Management]:::internal_component
        users_management_service[Users Management]:::internal_component     
      end
    end

    subgraph infra [Infrastructure]
        email_service(Email Service):::external_system
        message_brokers(Message Brokers):::external_system
    end

    subgraph centralized_observability [Observability]
        otel_collector[OTEL Collector]:::external_system
        observability_data_viewer[Logs, Traces, Metrics Viewer]:::external_system        
    end

    user --> |View and<br /> order books,<br /> and view user account|spa
    spa --> |View and<br /> order books,<br /> and view user account|api_gateway
    api_gateway --routes--> apis
    books_api -- read from, write to --> books_database
    orders_api -- read from, write to --> orders_database
    users_api -- read from, write to --> users_database
    orders_history_api -- read from, write to --> orders_history_database
    subscriptions_api -- read from, write to --> subscription_database
    
    orders_api -- sends<br />message:<br />order_created --> message_brokers
    subscriptions_api -- sends<br />message:<br />user_subscribed --> message_brokers
    orders_management_service -- read from, write to --> orders_database
    users_management_service -- read from, write to --> users_database
    
    message_brokers -- consumes<br />messages:<br /> order_created,<br />user_subscribed --> orders_management_service
    message_brokers -- consumes<br />messages:<br /> user_subscribed --> users_management_service

    apis --sends logs,<br /> traces and<br /> metrics--> centralized_observability
    services --sends logs,<br /> traces and<br /> metrics--> centralized_observability
    databases --sends logs,<br /> traces and<br /> metrics--> centralized_observability
    infra --sends logs,<br /> traces and<br /> metrics--> centralized_observability
```

## Learning Resources

- [C4 Models](https://c4model.com/)
