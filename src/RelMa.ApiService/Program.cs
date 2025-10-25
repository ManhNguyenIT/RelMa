using Microsoft.Extensions.FileProviders;
using RelMa.ApiService.Extensions;
using RelMa.Application;
using RelMa.Infrastructure;
using RelMa.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddOpenTelemetry();

builder.Services
    .AddApplication(builder.Configuration)
    .AddPresentation(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCors(builder.Configuration);
builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.UseExceptionHandler();

var assetsPath = Path.Combine(builder.Environment.ContentRootPath, "assets");
if (!Directory.Exists(assetsPath))
{
    Directory.CreateDirectory(assetsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(assetsPath)),
    RequestPath = "/assets"
});

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwaggerWithUi();
    app.UseDeveloperExceptionPage();

    await app.ApplyMigrations();
}

await app.RunAsync();
