# EFCoreSetup

A learning project for Entity Framework Core (Database-First) with ASP.NET Core Web API, used as the baseline for a separate CI/CD learning track with GitHub Actions.

## What this project is

A small ASP.NET Core Web API backed by SQL Server via EF Core, modeling a simple book catalog: `Book`, `Author`, `Language`, `CurrencyType`, `BookPrice`. The EF Core model (entity classes + `AppDBContext`) is generated from an existing SQL Server database using **Database-First scaffolding** — there are no EF Core Migrations in this project; the database is the source of truth, not the code.

## Technologies used

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core 10 (SQL Server provider), Database-First
- Swashbuckle / Swagger (OpenAPI docs + UI)
- xUnit (unit tests, `Microsoft.EntityFrameworkCore.InMemory` for DB-free testing)

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Developer Edition, or any reachable instance) — only required to run the API and hit its endpoints; not required to build or run the unit tests
- `dotnet-ef` CLI tool, only if you intend to re-scaffold the model after a schema change:
  ```bash
  dotnet tool install --global dotnet-ef --version 10.0.11
  ```

## Configuring SQL Server

The database (`BookCatalogDb`) must already exist — this project does not create or migrate it. Create it and its tables in SQL Server Management Studio (or `sqlcmd`) first.

## Configuring the connection string

The connection string lives in [`EFCoreSetup/EFCoreSetupApp/appsettings.json`](EFCoreSetup/EFCoreSetupApp/appsettings.json) under `ConnectionStrings:DefaultConnection`. It currently uses Windows Integrated Security (`Integrated Security=True`), so it contains no username or password — that's why it's safe to commit as-is.

If you ever switch to SQL Server authentication (a username/password), **do not put that in `appsettings.json`**. Use one of:
- **User Secrets** (recommended for local dev): `dotnet user-secrets init` then `dotnet user-secrets set "ConnectionStrings:DefaultConnection" "..."` from the `EFCoreSetupApp` folder.
- **Environment variable**: `ConnectionStrings__DefaultConnection` (the `__` maps to the `:` in config).
- A local, gitignored `appsettings.Local.json` (already covered by `.gitignore`).

`appsettings.Development.json` is committed and currently holds no secrets (logging config only) — keep it that way.

## Running EF Core "migrations"

There are none, by design. This project follows Database-First: the database schema is the source of truth, and the C# model is regenerated from it with:

```bash
cd EFCoreSetup/EFCoreSetupApp
dotnet ef dbcontext scaffold "<your-connection-string>" Microsoft.EntityFrameworkCore.SqlServer -o Data -c AppDBContext --context-dir Data --no-onconfiguring -f
```

Run this after changing the database schema directly (new table, new column, new FK) to regenerate the entity classes and `AppDBContext` to match.

## Running the application

```bash
cd EFCoreSetup/EFCoreSetupApp
dotnet run
```

Swagger UI is available at `/swagger` when running in the `Development` environment.

## Running tests

```bash
cd EFCoreSetup
dotnet test
```

Tests use EF Core's InMemory provider, not a real SQL Server instance — no database setup is required to run them.

## Building

```bash
cd EFCoreSetup
dotnet build
```

## Publishing

```bash
cd EFCoreSetup/EFCoreSetupApp
dotnet publish -c Release -o ./publish
```
