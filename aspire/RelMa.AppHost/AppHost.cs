using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
    .WithEnvironment("Redis__Configuration", builder.Configuration["Redis:Configuration"])
    .WithEnvironment("Redis__InstanceName", builder.Configuration["Redis:InstanceName"]);

var postgres = builder.AddConnectionString("DefaultConnection");

var keycloak = builder.AddExternalService("keycloak", new Uri("http://localhost:8080", UriKind.Absolute))
    .WithReferenceRelationship(postgres);

var apiService = builder.AddProject<Projects.RelMa_ApiService>("apiservice")
    .WithReference(cache).WaitFor(cache)
    .WithReference(postgres).WaitFor(postgres)
    .WithReference(keycloak).WaitFor(keycloak)
    .WithHttpHealthCheck("/health");

builder.AddExternalService("webapp", new Uri("http://127.0.0.1:3000",UriKind.Absolute))
    .WithReferenceRelationship(keycloak)
    .WithReferenceRelationship(apiService);

await builder.Build().RunAsync();
