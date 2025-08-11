# [Portal]
======================
## Project Overview
A .NET 9 Microservice Application leveraging Clean Architecture, Entity Framework Core, Domain-Driven Design (DDD) principles, and the CQRS pattern. This project showcases a modular, scalable architecture with a Minimal API for the web interface.

## Architectural Overview
- **Architecture**: Microservice with Clean Architecture
- **Layers**:
  - **Presentation**: RelMa.ApiService (Minimal API)
  - **Application**: RelMa.Application (CQRS, DDD)
  - **Domain**: RelMa.Domain (Entities, Events)
  - **Infrastructure**: RelMa.Infrastructure (Database, Caching)
- **Patterns & Principles**:
  - CQRS for Query and Command Handling
  - DDD for Domain Modeling
  - Entity Framework Core for Database Operations
  - Redis for Output Caching

## Detailed Project Structure
```plain
sdi
├── Core/               # Shared Core Libraries
├── Services/           # Main Application Source
│   ├── Portal/         # Service name (Example: Portal)
│   │   ├── RelMa.AppHost/      # App Host Configuration
│   │   ├── RelMa.ApiService/   # Minimal API
│   │   ├── RelMa.Application/  # Application Logic (CQRS)
│   │   ├── RelMa.Domain/       # Domain Models & Events
│   │   ├── RelMa.Infrastructure/ # Database, Caching, etc.
│   │   ├── RelMa.Shared/       # Shared Core Class and Interface
│   │   ├── RelMa.Test/         # Unit & Integration Tests
└── ...                 # Solution & Config Files
```

### Key Components

#### **RelMa.ApiService**
- **Type**: Minimal API
- **Example Endpoint**: Refer to `Program.cs` for API Endpoints

#### **RelMa.Application**
- **Use Cases**: Commands & Querries Handler
- **Behaviors**: Commands & Querries Behaviors

#### **RelMa.Domain**
- **Entities**: `TodoItem.cs`
- **Events**: `TodoItemCreatedDomainEvent.cs`

#### **RelMa.Infrastructure**
- **Database**: `ApplicationDbContext.cs`
- **Caching**: Redis Output Cache Configuration

## Features
- **CQRS with Command & Query Handlers**
- **Domain-Driven Design (DDD) with Events**
- **Entity Framework Core for Database Operations**
- **Redis for Output Caching**
- **FluentValidation for Request Validation**

## Prerequisites
- .NET 9 SDK
- Visual Studio Code
- C# Extension Pack
- .NET SDK
- Redis (for Caching)

## Installation & Usage
1. **Navigate**: `cd sdi/Services/Portal`
2. **Clone**: `git clone https://scm.devops.vnpt.vn/geoit.sdi/backend/services/congthongtin.git .`
3. **Restore**: `dotnet restore`
4. **Migrations (if new)**: 
```$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations add InitialCreate --context ApplicationDbContext --project .\RelMa.Infrastructure\ --output-dir Database/Migrations/ --startup-project    .\RelMa.ApiService\
dotnet ef database update --startup-project .\RelMa.ApiService\
```
5. **Run**: `dotnet run`

## Contributing
1. Fork Repository
2. Create Feature Branch
3. Ensure Tests Pass: `dotnet test`
4. Submit Pull Request with Detailed Description


### Deployment via Helm (Kubernetes)

#### Prerequisites
- **Kubernetes Cluster** (e.g., Minikube, GKE, AKS)
- **Helm Installed**

#### Steps to Deploy via Helm

1. **Navigate to Helm Directory**:
   ```bash
   cd cd sdi/Services/Portal
   ```
2. **Install/Upgrade Helm Release**:
   ```bash
   helm install --upgrade sdi-backend-portal-service RelMa.Deployment/Helm
   ```
   - **For a Specific Namespace (e.g., `geoit-sdi`)**:
     ```bash
     helm install --upgrade sdi-backend-portal-service RelMa.Deployment/Helm -n geoit-sdi
     ```
3. **Verify Deployment**:
   ```bash
   kubectl get deployments -n geoit-sdi
   kubectl get svc -n geoit-sdi
   ```
4. **Uninstall (if needed)**:
   ```bash
   helm uninstall sdi-backend-portal-service -n geoit-sdi
   ```

#### Customizing Deployment with `values.yaml`
- Edit `sdi/Services/Portal/RelMa.Deployment/Helm/values.yaml` to customize your deployment (e.g., image versions, resource limits).
- Example Customization:
  ```yaml
  # In values.yaml
  image:
    repository: your-custom-image-repo
    tag: latest
  ```


### Deployment to Kubernetes (Non-Helm, using YAML)

#### Prerequisites
- **Kubernetes Cluster** (e.g., Minikube, GKE, AKS)
- **kubectl Installed**

#### Steps to Deploy via YAML

1. **Navigate to YAML Directory**:
   ```bash
   cd sdi/Services/Portal
   ```
2. **Apply Configurations**:
   ```bash
   kubectl apply -f RelMa.Deployment/Yaml/configmap.yaml
   kubectl apply -f RelMa.Deployment/Yaml/deployment.yaml
   kubectl apply -f RelMa.Deployment/Yaml/service.yaml
   ```
   - **For a Specific Namespace (e.g., `geoit-sdi`)**:
     ```bash
     kubectl apply -f . -n geoit-sdi
     ```
3. **Verify Deployment**:
   ```bash
   kubectl get deployments -n geoit-sdi
   kubectl get svc -n geoit-sdi
   ```
4. **Delete Resources (if needed)**:
   ```bash
   kubectl delete -f . -n geoit-sdi
   ```

### Building Docker Image

#### Steps to Build Docker Image

1. **Navigate to Project Root** (assuming `Dockerfile` is at the project root):
   ```bash
   cd sdi
   ```
2. **Build the Docker Image**:
   ```bash
   docker build -f Services/Portal/RelMa.ApiService/Dockerfile -t sdi-backend-portal-service:0.0.1 .
   ```
3. **Run the Docker Container**:
   ```bash
   docker run -p 8080:8080 sdi-backend-portal-service:0.0.1
   ```

## License
MIT License
