# GroceryStore API

High-performance oriented backend API for grocery store inventory and product management.

The project demonstrates real-world backend engineering approaches including
Vertical Slice Architecture, hybrid data access (EF Core + Dapper),
and dynamic product metadata validation using PostgreSQL JSONB.

⚠️ Project is actively under development.
Authentication, Dockerization and inventory workflows are currently in progress.

## Tech Stack

**Backend**
- .NET 10 / ASP.NET Core Minimal APIs
- C#

**Database**
- PostgreSQL

**Data Access**
- Entity Framework Core (Write operations)
- Dapper (Optimized Read queries)

**API Documentation**
- Scalar

## Key Libraries

- FluentValidation — request validation
- ServiceScan.SourceGenerator — compile-time dependency registration
- IMemoryCache — caching rarely changing category schemas


## Key Features

### Dynamic Product Metadata (JSONB)
Products support flexible attributes depending on category hierarchy
(e.g. fat content, weight, origin).

Metadata is stored using PostgreSQL JSONB and validated dynamically
based on recursive category inheritance.

------------------------------------------------------------------------------

### Hybrid Data Access Strategy
- EF Core used for command operations (writes)
- Dapper used for high-performance read queries

This separation improves performance while maintaining development simplicity.

------------------------------------------------------------------------------

### Vertical Slice Architecture
Each feature is isolated and contains:
- Endpoint
- Request / Response DTO
- Validation
- Repository
- Domain logic

Minimizes coupling and improves scalability.

------------------------------------------------------------------------------

### Optimized JSONB Queries
Metadata extraction implemented using:

- LATERAL JOIN
- JSONB functions
- Recursive CTE

Allows relational querying without deserialization overhead.

------------------------------------------------------------------------------

### Data Integrity & Safety

The system prioritizes predictable data behavior and protection from accidental data loss.

- **Soft Deletion Strategy**  
  Products are logically deleted using an `is_active` flag.
  EF Core Global Query Filters automatically exclude inactive entities.

- **Foreign Key Safety**  
  All relationships use `Restrict` delete behavior
  to prevent unintended cascading deletions.

------------------------------------------------------------------------------

### Dynamic Validation & Normalization Pipeline
Validation and normalization logic is decoupled from the code and driven by metadata stored in the database.

- **Schema-Driven Rules**
Attribute constraints (types, ranges, required flags) are retrieved dynamically,
allowing schema updates without application redeployment.

- **Result Pattern**
Implementation: Replaces standard exception-based
flow with ValidationResult and NormalizationResult.
This enables:
  - Batch Error Collection: Multiple validation errors are returned in a single response.
  - Performance: Avoids expensive stack trace generation for predictable validation scenarios.

- **Data Canonicalization**
Incoming values are automatically normalized to a standard format
(e.g. decimal precision, boolean mapping) before being stored in JSONB.

- **Extensible Service Design**
Encapsulated logic allows adding new attributes or complex
validation types without modifying the core business services.

------------------------------------------------------------------------------

### Infrastructure Automation

Several infrastructure concerns are automated to reduce boilerplate and runtime overhead.

- **Snake_case Naming Convention**  
  Database tables, columns and foreign keys are automatically converted to
  PostgreSQL-friendly `snake_case` using a custom `ModelBuilder` extension.

- **Compile-Time Endpoint Registration**  
  Endpoints and services are registered automatically via
  `IEndpoint` + Source Generators, eliminating reflection
  and reducing manual configuration inside `Program.cs`.
  
------------------------------------------------------------------------------

## Roadmap

### Security
- ASP.NET Identity
- JWT Authentication
- Role-based authorization

### Inventory
- FIFO batch processing
- Stock tracking
- Batch lifecycle management

### DevOps
- Docker support
- Logging (Serilog)
- Integration tests


### Getting Started

1.  **Clone the repository**:
    `git clone [https://github.com/9asmodey6/GroceryStore.git](https://github.com/9asmodey6/GroceryStore.git)
    cd GroceryStore`

2.  **Configure the database**:
    Update the `ConnectionStrings` in `appsettings.json` or user_secrets with your PostgreSQL credentials:
    `"ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=grocery_db;Username=postgres;Password=YourPassword"
    }`

3.  **Apply Migrations**:
    `dotnet ef database update`

4.  **Run the application**:
    `dotnet run --project GroceryStore` 

5.  **Access API Documentation**:
    Open `http://localhost:5000/scalar/v1` (or your configured port) to view the interactive API docs.
    **Note:** Currently, the database starts empty. A seeding script is planned for future updates to automatically provide a set of categories and attributes during migration.
