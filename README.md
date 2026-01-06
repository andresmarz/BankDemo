# BankDemo API

Technical assessment project built with .NET 9, focused on clean architecture, business rules and idempotent transfers.

## Architecture

The solution follows a clean architecture approach:

- **API**: HTTP layer (controllers, DTOs, Swagger)
- **Application**: Use cases and repository interfaces
- **Domain**: Business entities and domain rules
- **Infrastructure**: Data access using Dapper and SQL Server

## Main Features

- Create transfers between accounts and beneficiaries
- Idempotent transfer creation using `clientRequestId`
- Daily transfer limits per currency
- Clear separation of concerns
- OpenAPI documentation using Swagger

## Database

Database structure is defined using versioned SQL scripts located in the `database` folder:

- `001_schema.sql`: tables, indexes and constraints
- `002_seed.sql`: demo data for local testing

The project uses SQL Server and Dapper (no ORM migrations).

## Running the project (optional)

1. Create a SQL Server database named `BankDemo`
2. Execute the SQL scripts in order:
   - `database/001_schema.sql`
   - `database/002_seed.sql`
3. Update the connection string in `appsettings.json` if needed
4. Run the API project
5. Open Swagger at `/swagger`

## Notes

This project prioritizes clarity, explicit business rules and maintainable architecture over framework automation.
