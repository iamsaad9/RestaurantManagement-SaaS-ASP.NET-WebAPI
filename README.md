# DemoApplication — Restaurant Management SaaS (ASP.NET Web API)

Short documentation for the DemoApplication Web API (Restaurant Management SaaS).

## Overview

DemoApplication is an ASP.NET Core Web API for managing restaurants, tables, and reservations. It includes:

- JWT-based authentication and authorization
- EF Core-backed data model and migrations
- Controller-based API endpoints for restaurants, tables, and reservations
- Services layer with dependency injection

This repository contains the server-side API portion of a small SaaS-style restaurant management system.

## Tech Stack

- .NET 10 (target framework net10.0)
- ASP.NET Core Web API
- Entity Framework Core (migrations present in `Migrations/`)
- JWT for authentication (see `Services/JwtService.cs`)

## Repository Layout

- `DemoApplication/` — main project source
  - `Controllers/` — API controllers (`AuthController.cs`, `RestaurantController.cs`, `TableController.cs`, `ReservationController.cs`)
  - `Data/` — `AppDbContext.cs`
  - `Migrations/` — EF Core migrations
  - `Models/` — DTOs and entity definitions
  - `Services/` — application services (JWT, reservation, restaurant, table services)
  - `Middleware/` — global middleware (e.g., `ExceptionMiddleware.cs`)
  - `Program.cs` — application entry and DI configuration
  - `appsettings.json` / `appsettings.Development.json` — configuration files

## Getting Started

Prerequisites:

- .NET 10 SDK (install from Microsoft if not present)
- A supported database (update the connection string in `DemoApplication/appsettings.json`) — typically SQL Server, SQLite, or other provider configured in `AppDbContext`.

Quick run (from repository root):

```powershell
cd DemoApplication
dotnet restore
dotnet build
dotnet run
```

If the application uses EF Core migrations, apply them before first run (update the connection string first):

```powershell
cd DemoApplication
dotnet tool install --global dotnet-ef --version 8.*   # if dotnet-ef not installed
dotnet ef database update
```

Configuration notes:

- Edit `DemoApplication/appsettings.json` or environment-specific files to set `ConnectionStrings:DefaultConnection` and any JWT settings (key, issuer, expiry).

## Database & Migrations

- Migrations are stored under `DemoApplication/Migrations/`.
- To create a new migration:

```powershell
cd DemoApplication
dotnet ef migrations add <Name>
dotnet ef database update
```

If you use a SQL Server LocalDB connection locally, a typical connection string looks like:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DemoApplicationDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Adjust as needed for your environment.

## Authentication

Authentication is implemented using JWT. See `Services/JwtService.cs` for token generation logic and `AuthController.cs` for login/register endpoints (or however your project wires them).

Typical flow:

- Register a user (if registration is enabled) → receive/login credentials
- Log in with credentials → receive JWT access token
- Provide the token to protected endpoints via the `Authorization: Bearer <token>` header

## API Endpoints (high level)

Primary controllers are in `DemoApplication/Controllers/`:

- `AuthController` — authentication (login, register, token)
- `RestaurantsController` — restaurant CRUD and listing
- `TableController` — table CRUD and availability operations
- `ReservationController` — create/list/cancel reservations

For exact routes and sample requests, inspect the controller source files in `DemoApplication/Controllers/` or use the included `DemoApplication/DemoApplication.http` file which contains example requests.

Example cURL (replace host/port as appropriate):

```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"user@example.com","password":"P@ssw0rd"}'
```

## Services & Business Logic

- Business logic lives in `DemoApplication/Services/`.
- Interfaces for core services are under `DemoApplication/Interfaces/` and their implementations under `Services/` (e.g., `ReservationService`, `RestaurantService`, `TableService`).

## Middleware

- `Middleware/ExceptionMiddleware.cs` provides centralized exception handling and consistent error responses.

## Development Notes

- Launch profiles are in `DemoApplication/Properties/launchSettings.json`.
- Static assets and localization resources are produced into `bin/Debug/net10.0/` during build.
- If you encounter 500s, check logs and ensure `ConnectionStrings` and JWT secret values are set.

## Testing

There are no dedicated test projects in this repository by default. To add unit or integration tests, create a test project and reference `DemoApplication`.

## Contributing

- Fork the repo, create a feature branch, add tests for new behavior, and open a PR describing the change.

## Troubleshooting

- If `dotnet run` fails with EF or connection errors, confirm the connection string, DB server availability, and that migrations were applied.
- Check `appsettings.Development.json` for overrides when running in Development environment.

## Useful Links in this repo

- Controllers: [DemoApplication/Controllers](DemoApplication/Controllers)
- Program/Startup: [DemoApplication/Program.cs](DemoApplication/Program.cs)
- EF Migrations: [DemoApplication/Migrations](DemoApplication/Migrations)
- Example HTTP requests: [DemoApplication/DemoApplication.http](DemoApplication/DemoApplication.http)

---

If you'd like, I can:

- expand the API section with full route/method/request/response examples
- generate an OpenAPI/Swagger doc or Postman collection
- add a short developer README inside `DemoApplication/` with local-db quickstart

Tell me which of those you'd like next.
