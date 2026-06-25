# Configuration & Secret Management Guide

## Overview

This project follows a security-first configuration strategy:

| Environment          | Secret Source                        |
| -------------------- | ------------------------------------ |
| Local Development    | User Secrets                         |
| Staging              | Environment Variables                |
| Production           | Environment Variables / Secret Store |
| Source Control (Git) | No Secrets                           |

The application uses ASP.NET Core's built-in configuration system, allowing different configuration providers to override each other.

Configuration priority (highest wins):

```text
Command Line Arguments
Environment Variables
User Secrets (Development Only)
appsettings.{Environment}.json
appsettings.json
```

---

# Why Secrets Should Never Be Stored in appsettings.json

## Bad Example

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=AppDb;User Id=sa;Password=SuperSecret123;"
  },
  "Jwt": {
    "Key": "MyVerySecretKey"
  }
}
```

Problems:

* Secrets are committed to Git.
* Secrets exist forever in Git history.
* Every developer receives production credentials.
* Repository leaks expose credentials.
* Password rotation becomes difficult.

Even private repositories should be treated as potentially exposed.

---

# What Can Be Stored in appsettings.json

Non-sensitive configuration is acceptable:

```json
{
  "Application": {
    "Name": "MyApi"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

For secrets, leave placeholders:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "Jwt": {
    "Key": ""
  }
}
```

---

# Local Development Setup

## Step 1 - Initialize User Secrets

Navigate to the API project directory:

```bash
dotnet user-secrets init
```

This creates a User Secrets ID inside the project file:

```xml
<PropertyGroup>
  <UserSecretsId>xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx</UserSecretsId>
</PropertyGroup>
```

Do not modify this value manually.

---

## Step 2 - Add Secrets

### Database Connection

```bash
dotnet user-secrets set 'ConnectionStrings:DefaultConnection' 'Server=localhost;Database=AppDb;Trusted_Connection=True;TrustServerCertificate=True;'
```

### JWT Secret

```bash
dotnet user-secrets set 'Jwt:Key' 'your-development-secret-key'
```

### Email API Key

```bash
dotnet user-secrets set 'Email:ApiKey' 'your-api-key'
```

---

## Step 3 - Verify

```bash
dotnet user-secrets list
```

Example:

```text
ConnectionStrings:DefaultConnection = Server=localhost;Database=AppDb;...
Jwt:Key = your-development-secret-key
Email:ApiKey = your-api-key
```

---

# Using Secrets in Code

No special code is required.

ASP.NET Core automatically loads User Secrets in Development.

```csharp
var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
```

Reading other configuration values:

```csharp
var jwtKey =
    builder.Configuration["Jwt:Key"];

var emailApiKey =
    builder.Configuration["Email:ApiKey"];
```

---

# Staging & Production Configuration

Production secrets should come from environment variables or a dedicated secret manager.

## Connection String

Windows:

```cmd
setx ConnectionStrings__DefaultConnection "Server=prod-db;Database=AppDb;User Id=app;Password=StrongPassword;"
```

Linux:

```bash
export ConnectionStrings__DefaultConnection="Server=prod-db;Database=AppDb;User Id=app;Password=StrongPassword;"
```

Notice:

```text
:
```

becomes

```text
__
```

Example mapping:

```text
ConnectionStrings:DefaultConnection
```

becomes

```text
ConnectionStrings__DefaultConnection
```

---

## JWT Key

Windows:

```cmd
setx Jwt__Key "production-secret-key"
```

Linux:

```bash
export Jwt__Key="production-secret-ke
y"
```

---

# Docker Example

```yaml
services:
  api:
    image: my-api
    environment:
      ASPNETCORE_ENVIRONMENT: Production
      ConnectionStrings__DefaultConnection: Server=db;Database=AppDb;User Id=app;Password=Password123;
      Jwt__Key: production-secret-key
```

---

# Kubernetes Example

Never place secrets directly in deployment manifests.

Use Kubernetes Secrets.

```yaml
env:
- name: ConnectionStrings__DefaultConnection
  valueFrom:
    secretKeyRef:
      name: api-secrets
      key: db-connection
```

---

# New Developer Onboarding

When a developer joins the project:

## 1. Clone Repository

```bash
git clone <repository>
```

---

## 2. Install Required Software

* .NET SDK
* SQL Server / PostgreSQL
* Visual Studio / Rider / VS Code

---

## 3. Initialize User Secrets

```bash
dotnet user-secrets init
```

Only required if the project does not already contain a UserSecretsId.

---

## 4. Obtain Development Secrets

Secrets should be provided through:

* Password manager
* Secure internal documentation
* Team lead
* DevOps team

Never through:

* Git
* Email
* Teams screenshots
* Slack screenshots

---

## 5. Configure Secrets

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<connection-string>"
```

```bash
dotnet user-secrets set "Jwt:Key" "<jwt-key>"
```

---

## 6. Verify Configuration

```bash
dotnet user-secrets list
```

---

## 7. Run Application

```bash
dotnet run
```

---

# Recommended Repository Structure

```text
src/
 ├── Api/
 │    ├── appsettings.json
 │    ├── appsettings.Development.json
 │    └── Program.cs
 │
 └── Infrastructure/

docs/
 └── configuration.md
```

---

# Configuration Checklist

Before deploying:

* [ ] No secrets inside appsettings.json
* [ ] No secrets inside appsettings.Development.json
* [ ] No secrets committed to Git
* [ ] Connection string comes from configuration provider
* [ ] JWT secrets come from configuration provider
* [ ] Production uses environment variables or secret store
* [ ] Secret rotation process documented
* [ ] New developer onboarding documented

---

# Future Improvements

As the application grows, consider moving production secrets to:

* Azure Key Vault
* AWS Secrets Manager
* HashiCorp Vault
* Kubernetes Secrets

This allows:

* Secret rotation
* Access auditing
* Centralized management
* Reduced operational risk

For most small-to-medium ASP.NET Core applications, User Secrets for development and Environment Variables for staging/production is an excellent long-term architecture.
