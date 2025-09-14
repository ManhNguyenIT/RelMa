using Microsoft.Extensions.FileProviders;
using RelMa.ApiService.Extensions;
using RelMa.Application;
using RelMa.Infrastructure;
using RelMa.Shared;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddApplication(builder.Configuration)
    .AddPresentation(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddHealthChecks();
builder.Services.AddCors(builder.Configuration);
builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.UseExceptionHandler();

var staticsPath = Path.Combine(builder.Environment.ContentRootPath, "statics");
if (!Directory.Exists(staticsPath))
{
    Directory.CreateDirectory(staticsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticsPath),
    RequestPath = "/statics"
});

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestContextLogging();
app.UseSerilogRequestLogging();

app.MapEndpoints();
app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwaggerWithUi();
    await app.ApplyMigrations();
}
try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (OperationCanceledException ex)
{
    Log.Information(ex, "Application was canceled: {Message}", ex.Message);
    await app.StopAsync();
}
catch (InvalidOperationException ex)
{
    Log.Fatal(ex, "An invalid operation occurred during bootstrapping");
    await app.StopAsync();
}
catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
{
    Log.Fatal(ex, "An unexpected error occurred during bootstrapping");
    await app.StopAsync();
}
finally
{
    await Log.CloseAndFlushAsync();
    await app.DisposeAsync();
}
