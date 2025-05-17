var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak" ,8076)
    .WithExternalHttpEndpoints()
    .WithDataVolume();

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithPgWeb();
var postgresDb = postgres.AddDatabase("weatherforcasthistory");

var kafka = builder.AddKafka("kafka")
    .WithDataVolume(isReadOnly: false)
    .WithKafkaUI();

builder.AddProject<Projects.WebApi>("apiservice")
    .WithExternalHttpEndpoints()
    .WithReference(postgresDb)
    .WithReference(keycloak)
    .WithReference(kafka)
    .WaitFor(postgresDb)
    .WaitFor(kafka)
    .WaitFor(keycloak);

var migrationService = builder.AddProject<Projects.MigrationService>("migrations")
    .WithReference(postgresDb)
    .WaitFor(postgresDb);

builder.AddProject<Projects.Consumer>("consumerservice")
    .WithReference(postgresDb)
    .WaitFor(postgresDb)
    .WithReference(kafka)
    .WaitFor(kafka);

await builder.Build().RunAsync();