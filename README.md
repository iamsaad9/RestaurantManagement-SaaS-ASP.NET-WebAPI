# DemoApplication — Restaurant Management SaaS (ASP.NET Core Web API)

This repository contains the server-side API for DemoApplication, a small SaaS-style restaurant management system for managing restaurants, tables, memberships, and reservations.

**Highlights:** JWT auth, EF Core persistence, FluentValidation, localization, and ready-to-run PostgreSQL migration.

## Overview

`DemoApplication` is an ASP.NET Core Web API that provides endpoints and services to manage restaurants, tables, user memberships, and reservations. The project follows a layered architecture with controllers, services, data access (EF Core), and middleware.

Core capabilities:

- JWT-based authentication and authorization
- EF Core-backed data model with migrations
- Controller-based API endpoints for restaurants, tables, and reservations
- Services layer with dependency injection and interfaces in `DemoApplication/Interfaces`

## Tech Stack

- .NET 10 (target framework `net10.0`)
- ASP.NET Core Web API
- Entity Framework Core (migrations in `DemoApplication/Migrations`)
- FluentValidation for DTO validation
- JWT tokens for authentication (see `DemoApplication/Services/JwtService.cs`)

## Repository Layout

- `DemoApplication/` — main project source
  - `Controllers/` — API controllers (e.g., `AuthController.cs`, `RestaurantsController.cs`, `TableController.cs`, `ReservationController.cs`)
  - `Data/` — `AppDbContext.cs` and EF Core configuration
  - `Migrations/` — EF Core migrations (Postgres-ready migration included)
  - `Models/` — DTOs and entity definitions (`Entities/`, `Dtos/`)
  - `Services/` — application services (JWT, reservation, restaurant, table services)
  - `Interfaces/` — service interfaces
  - `Middleware/` — global middleware (e.g., `ExceptionMiddleware.cs`)
  - `Validators/` — FluentValidation validators for DTOs
  - `Program.cs` — application entry and DI configuration
  - `appsettings.json` / `appsettings.Development.json` — configuration files

## What's New (recent additions)

- **FluentValidation validators:** New `Validators/` folder contains validators for incoming DTOs: `CreateReservationValidator`, `CreateRestaurantValidator`, `CreateTableValidator`, `LoginValidator`, `RegisterValidator` — improving input validation and API feedback.
- **Global exception handling:** `Middleware/ExceptionMiddleware.cs` centralizes error handling and returns consistent API error responses.
- **Postgres migration:** Initial PostgreSQL migration included (`Migrations/20260514152444_InitialPostgres.cs`) to ease deploying with PostgreSQL.
- **Membership & restaurant switching:** `Models/Entities/Membership.cs` and `Models/Dtos/SwitchRestaurantDto.cs` add user membership relationships and support switching the active restaurant context for a user.
- **Reservation querying:** `Models/Dtos/QueryDtos/ReservationQueryDto.cs` enables advanced list filtering, date ranges, status filters, and pagination for reservation endpoints.
- **Localization resources:** Build output contains multiple culture resource folders (under `bin/Debug/net10.0/`) enabling localized messages and resource-based texts.
- **Updated example requests:** `DemoApplication/DemoApplication.http` has been refreshed with sample requests covering the new endpoints and features.

## Getting Started

Prerequisites:

- .NET 10 SDK
- A database (configure `ConnectionStrings:DefaultConnection` in `DemoApplication/appsettings.json`). The project is compatible with SQL Server, SQLite, and PostgreSQL (migration included).

Quick run (from repository root):

```powershell
cd DemoApplication
dotnet restore
dotnet build
dotnet run
```

If you need EF Core tools (first run):

```powershell
cd DemoApplication
dotnet tool install --global dotnet-ef --version 8.*
dotnet ef database update
```

Configuration notes:

- Edit [DemoApplication/appsettings.json](DemoApplication/appsettings.json) to set `ConnectionStrings:DefaultConnection` and JWT configuration (`Jwt:Key`, `Jwt:Issuer`, expiry settings).

## Database & Migrations

- Migrations live in `DemoApplication/Migrations/`.
- Create a new migration:

```powershell
cd DemoApplication
dotnet ef migrations add <Name>
dotnet ef database update
```

Example LocalDB connection string (adjust as needed):

```
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\\\mssqllocaldb;Database=DemoApplicationDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

## Authentication

Authentication uses JWT. See `DemoApplication/Services/JwtService.cs` and `DemoApplication/Controllers/AuthController.cs` for token generation and auth endpoints.

Typical flow:

- Register (if enabled) → receive credentials
- Login → receive JWT access token
- Send `Authorization: Bearer <token>` header to protected endpoints

## API Endpoints (high level)

Primary controllers (see source for full routes):

- `AuthController` — authentication (login, register, token)
- `RestaurantsController` — restaurant CRUD and listing
- `TableController` — table CRUD and availability checks
- `ReservationController` — create, list (with query DTOs), and cancel reservations

Use [DemoApplication/DemoApplication.http](DemoApplication/DemoApplication.http) for working examples and quick testing.

## Services & Business Logic

- Business logic is implemented in `DemoApplication/Services/` and exposed via interfaces in `DemoApplication/Interfaces/` (e.g., `IReservationService`, `IRestaurantService`, `ITableService`).

## Middleware

- `Middleware/ExceptionMiddleware.cs` handles exceptions and returns a consistent JSON error shape.

## Development Notes

- Launch profiles: `DemoApplication/Properties/launchSettings.json`.
- Localization and static assets are produced under `bin/Debug/net10.0/` when building.
- If you encounter 500 responses, verify `ConnectionStrings` and JWT secret values.

## Testing

No test projects are included by default. To add tests, create a new test project and reference `DemoApplication`.

## Contributing

- Fork, add a feature branch, include tests for new behavior, and open a PR with a clear description.

## Troubleshooting

- If `dotnet run` fails with EF or connection errors, confirm DB availability and that migrations were applied.
- Check `appsettings.Development.json` for environment-specific overrides.

## Useful Links

- Controllers: [DemoApplication/Controllers](DemoApplication/Controllers)
- Program/Startup: [DemoApplication/Program.cs](DemoApplication/Program.cs)
- EF Migrations: [DemoApplication/Migrations](DemoApplication/Migrations)
- Example HTTP requests: [DemoApplication/DemoApplication.http](DemoApplication/DemoApplication.http)

---

If you'd like, I can:

- expand the API section with full route/method/request/response examples
- generate an OpenAPI/Swagger doc or Postman collection
- add a short developer README inside DemoApplication with a local-db quickstart

Tell me which of those you'd like next.
