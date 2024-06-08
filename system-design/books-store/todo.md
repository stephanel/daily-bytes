# TODO
# https://marketplace.visualstudio.com/items?itemName=usernamehw.todo-md

(A) Update in README file #important
(A) Init projects structure {cm:2024-05-12}
(A) create docker file: grafana {cm:2024-05-12}
(A) create docker file: postgresql {cm:2024-05-12}
(A) create docker file: rabbitmq {cm:2024-05-12}
(A) Create a common TestFramework #important {cm:2024-05-13} #backend
(A) Init Books integration tests project #important {cm:2024-05-13} #backend
(B) Create Books API project {cm:2024-05-13} #backend
(A) Init Books unit tests project #important #backend {cm:2024-05-14}
(A) Add Books.Infrastructure layer {due:2024-05-14} #backend {cm:2024-05-15}
(A) Setup dev databases {cm:2024-05-15}
(A) Create first migration and update integration tests {due:2024-05-15} {cm:2024-05-15}
(A) Setup Serilog. #important #observability {due:2024-05-16} {cm:2024-05-16}
(A) Init Angular UI #frontend #important {due:2024-05-16} {cm:2024-05-17}
(A) GetBooks API implementation #backend #important {due:2024-05-16} {cm:2024-05-16}
(A) GetBooks API integration #frontend #important {due:2024-05-16} {cm:2024-05-16}
(A) GetBook API implementation #backend #important {due:2024-05-17} {cm:2024-05-17}
(A) GetBook API integration #frontend #important {due:2024-05-17} {cm:2024-05-17}
(A) Refactor Common.Extensions - split and Common.Extensions.API #backend #important {due:2024-05-18} {cm:2024-05-18}
(A) Add VerifyTests to Integration Tests {due:2024-05-18} {cm:2024-05-18}
(A) Configure backend tracing #important #observability {due:2024-05-18} {cm:2024-05-18}
(A) Configure backend metrics #important #observability {due:2024-05-18} {cm:2024-05-18}
(A) Configure logs/trace/metrics forwarding to grafana #important #observability {due:2024-05-18} {cm:2024-05-19}
(A) Write logs to grafana, or alt. #important #observability {cm:2024-05-18}
(A) Add books to database (db migration) #important #observability {due:2024-05-18} {cm:2024-05-19}
(A) Init React UI #frontend #important {cm:2024-05-20}
(A) Update integration tests - add DB fixture {due:2024-05-20} {cm:2024-05-21}
(A) add an API gateway #backend #important {due:2024-05-25} {cm:2024-05-25}
(A) Add Auth service - cookie based authentication #backend #important {due:2024-06-01} {cm:2024-05-31}
(A) Add Auth service configuration to API Gateway #backend #important {due:2024-06-01} {cm:2024-05-31}
(A) Add Auth service - add bearer token authentication #backend #important {due:2024-06-01} {cm:2024-06-01}
(A) Add auth guard - angular #frontend #important {due:2024-06-01} {cm:2024-06-01}
(A) grafana or aspire-dashboard or or seq/jaeger? #important {due:2024-05-19} {cm}
(B) send traces to grafana, or alt. #important #observability {cm}
(C) Init Vue.js UI #frontend {cm}
(A) DevOps - docker setup to run the BooksApi and Auth in docker #devops #backend {due:2024-06-04} {cm}
(A) DevOps - docker setup - add ApiGateway #devops #backend {due:2024-06-04} {cm}

(A) DevOps - docker setup - add db migration  #devops #backend {due:2024-06-06}
(A) Auth Service - fix healthcheck endpoint through API Gateway - base path is 'identity'  #devops #backend {due:2024-06-06}

(A) Add auth guard - react #frontend #important {due:2024-06-08}
(A) frontend Http errors handling #frontend #important {due:2024-05-18}

(A) Add API versioning  #backend #important

(B) Adapt integration tests: - run the different APIs, including the API gateway in docker (using fixtures) - change the tests to call through the API gateway #backend #important {due:2024-05-02}

(C) frontend angular - Http call retry/timeout mechanisms #frontend

(D) DevOps - docker setup - add https support #devops #backend

(E) explore a better way to register services dependencies - https://stackoverflow.com/a/72125775

(F) books: add publisher, publish date, number of pages, subjects
(F) Make author clickable in books list and show details page for selected author with the list of written books #enhancement #frontend


(Z) Make logs/traces/metrics configurable.

(Z) Init solution for monolith implementation

(Z) customize labels in Loki