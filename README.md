# MelliMaharat

University database project (MelliMaharat). This repository contains a database (MDF/LDF) and a .NET solution named `DataForge` that models the university domain (lessons, students, masters, presentations, selections, etc.). The repository is structured so `DataForge` is the main solution and there are two application surface projects that will use it:

- `DataForge.WebApi` — an ASP.NET Core Web API project (server-side API for the university application)
- `DataForge.Wpf` — a WPF desktop application (client) that consumes the DAL via the shared projects

Both applications follow a Database-First or Code-First approaches using the provided database. This README explains the repository layout, how to open and build the `DataForge` solution, how to connect to the provided database files, run tests, and next steps for the WebApi and WPF projects.

## Repository layout

- `Database/` - Contains the SQL Server database files shipped with the repo:
  - `MelliMaharatDB.mdf`
  - `MelliMaharatDB_log.ldf`
- `DataForge/` - Main .NET solution and projects that model the database and data access
  - `DataForge.slnx` - Visual Studio / dotnet solution
  - `DataForge.Dal/` - EF Core DbContext, migrations, repository code and data access layer
    - `appsettings.configuration.json` - contains (or is expected to contain) the connection string used by the DAL and design-time factory
    - `DbContexts/ApplicationDbContext.cs` - the EF Core DbContext
    - `Migrations/` - EF Core Migrations (present in the project even though you're using Database-First)
  - `DataForge.Models/` - POCO entities and EF configurations
  - `DataForge.Seeding/` - helper seeding code (if present)
  - `DataForge.Tests/` - unit tests for the data layer and models
- `DataForge.WebApi/` - ASP.NET Core Web API project (server) — consumes `DataForge.Dal` and `DataForge.Models`
- `DataForge.Wpf/` - WPF desktop client project — consumes `DataForge.Dal` and `DataForge.Models`
 - `Desktop/` and `Website/` are left as legacy placeholders (if present) or can be removed once `DataForge.WebApi` and `DataForge.Wpf` are added.

## Project purpose and approach

The repository represents a university database (MelliMaharat). You and your teammate will implement two applications that use this database. You are working with a Database-First approach: the database is primary (MDF/LDF files are included or available), and the EF Core model is built from that database (or the provided models are already scaffolded).

Key points:
- Use the `DataForge` projects as the shared data access layer. Both the Website and Desktop projects should reference `DataForge.Dal` and `DataForge.Models` to interact with the data.
- The repository already contains EF Core Migrations and a DbContext. If you prefer, you can scaffold models from the database with `dotnet ef dbcontext scaffold` for a fresh Database-First workflow.

## Getting started (quick)

Prerequisites:

- .NET SDK 10
- Visual Studio 2026 or VS Code with C# DEV Kit extension
- SQL Server or SQL Server Express / LocalDB / Azure Data Studio to attach the `.mdf` file

1. Open the solution

   - Using Visual Studio: double-click `DataForge/DataForge.slnx`.
   - Using the CLI (PowerShell, for Bash/zsh replace `;` with `&&`):

```powershell
cd "c:/Github/MelliMaharat/DataForge";
dotnet restore;
dotnet build
```

2. Attach or connect to the database

   - Preferred (GUI): Open SQL Server Management Studio (SSMS) and attach `Database/MelliMaharatDB.mdf` to a local SQL Server instance.
   - Using LocalDB (example) you can create an `.
` connection string that points to the attached database or start a new LocalDB instance and attach the MDF.

   After attaching the DB, update the connection string in `DataForge.Dal/appsettings.configuration.json` (or in your secrets/user settings) so that `ApplicationDbContext` can connect.

3. Run tests

   From the `DataForge` folder you can run unit tests:

```powershell
cd "c:/Github/MelliMaharat/DataForge";
dotnet test "DataForge.Tests/DataForge.Tests.csproj"
```

4. Run or develop the WebApi / WPF projects

   - Open or create the `DataForge.WebApi` and `DataForge.Wpf` projects. Both should reference `DataForge.Dal` and `DataForge.Models`.
   - Example (WebApi): in `Program.cs` register `ApplicationDbContext` and the repositories from `DataForge.Dal` using the same connection string used in the DAL.
   - Example (WPF): add references to `DataForge.Dal` and `DataForge.Models` and use view models that call repositories or a service layer exposed by the WebApi.

## Working with the DataForge DAL

- Connection string location: `DataForge.Dal/appsettings.configuration.json` (update for your local environment).
- Design-time factory: `DataForge.Dal/DbContexts/ApplicationDbContextFactory.cs` exists to help EF tools run migrations or scaffolding commands.
- If you need to re-scaffold models from the database (database-first):

```powershell
cd "c:/Github/MelliMaharat/DataForge/DataForge.Dal";
# Example (adjust provider and connection string):
dotnet ef dbcontext scaffold "Server=(localdb)\\mssqllocaldb;Database=MelliMaharatDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o ..\\DataForge.Models\\Scaffolded -f
```

Note: change the connection string to match how you attached the MDF or use Code-First Approach using `Database=DataForge` or any other name you prefer.

## Migrations

The `DataForge.Dal/Migrations` folder contains migrations. If you are using Database-First you may not need to run `dotnet ef database update`, but if you regenerate the model or update schema you can use EF migrations. Ensure the connection string targets the environment you want to modify.

## Seeding

If you want to seed data programmatically, see `DataForge.Seeding/DataSeeder.cs` (if present). When running your applications you can call the seeder at startup to ensure test data exists.

## Tests

- Unit tests live in `DataForge.Tests`. Use `dotnet test` as shown above. Tests usually expect a database to be present or an in-memory configuration — read the test code in `DataForge.Tests/UnitTests` to understand exact requirements.

## Development workflow suggestions

- Keep `DataForge.Dal` and `DataForge.Models` as the single source of truth for data access.
- The Website and Desktop apps should be thin: use dependency injection and repositories from the DAL.
- For database changes, prefer making schema changes in a development database, then generate migrations and commit them into `Migrations/`.

## Common commands (PowerShell)

```powershell
# restore & build
cd "c:/Github/MelliMaharat/DataForge"; dotnet restore; dotnet build

# run tests
dotnet test "DataForge.Tests/DataForge.Tests.csproj"

# scaffold (example)
cd "c:/Github/MelliMaharat/DataForge/DataForge.Dal"; dotnet ef dbcontext scaffold "<Your-Connection-String>" Microsoft.EntityFrameworkCore.SqlServer -o ..\\DataForge.Models\\Scaffolded -f
```

## Next steps for you and your teammate

1. Create or open `DataForge.WebApi` (ASP.NET Core Web API) and `DataForge.Wpf` (WPF) projects and add them to the solution if not present.
2. In both projects, reference `DataForge.Dal` and `DataForge.Models` as the shared data layer.
3. In `DataForge.WebApi` register `ApplicationDbContext` in DI and expose REST endpoints for Students, Lessons, Selections, etc.
4. In `DataForge.Wpf` implement an MVVM client that either calls the WebApi or directly uses the repositories (for offline scenarios).
5. Agree on connection string handling (user secrets, environment variables, or per-machine appsettings) and document it in `DataForge.Dal/appsettings.configuration.json` or repository wiki.

## Contributing

Feel free to create issues and pull requests. Keep database changes explicit and include migration files when modifying schema. Document any changes to the seeding or connection setup.
