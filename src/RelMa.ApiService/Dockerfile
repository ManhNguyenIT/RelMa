# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/RelMa.ApiService/RelMa.ApiService.csproj", "src/RelMa.ApiService/"]
COPY ["src/RelMa.Shared/RelMa.Shared.csproj", "src/RelMa.Shared/"]
COPY ["src/RelMa.Application/RelMa.Application.csproj", "src/RelMa.Application/"]
COPY ["src/RelMa.Domain/RelMa.Domain.csproj", "src/RelMa.Domain/"]
COPY ["src/RelMa.Infrastructure/RelMa.Infrastructure.csproj", "src/RelMa.Infrastructure/"]
RUN dotnet restore "./src/RelMa.ApiService/RelMa.ApiService.csproj"
COPY . .
WORKDIR "/app/src/RelMa.ApiService"
RUN dotnet build "./RelMa.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RelMa.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/assets/originals /app/assets/thumbnails && \
    chown -R 1000:1000 /app/assets && \
    chmod -R 775 /app/assets
ENTRYPOINT ["dotnet", "RelMa.ApiService.dll"]