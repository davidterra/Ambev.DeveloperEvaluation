# Developer Evaluation Solution

## Overview
This solution implements an API for managing sales records, following Domain-Driven Design (DDD) principles. It includes CRUD operations for sales and related entities, such as customers, branches, and products. The solution also enforces business rules for discounts and item limits, and optionally logs events for sale lifecycle changes.

## Features
- **Sales Management**: Create, read, update, and delete sales records.
- **Business Rules**:
  - Discounts based on item quantities.
  - Maximum item limits per sale.
- **Event Logging**: Logs events for `SaleCreated`, `SaleModified`, `SaleCancelled`, and `ItemCancelled`.
- **Pagination, Filtering, and Ordering**: Supports advanced query capabilities for list endpoints.
- **Error Handling**: Provides detailed error responses with HTTP status codes.

## Tech Stack
- **Backend**: ASP.NET Core (.NET 8)
- **Database**: Entity Framework Core with In-Memory and Postgres support
- **Testing**: xUnit, FluentValidation, and integration tests with EF Core
- **Other Libraries**:
  - FluentValidation for input validation
  - Bogus for test data generation
  - NSubstitute for mocking in tests

## Project Structure
The solution is organized into the following projects:

1. **Ambev.DeveloperEvaluation.WebApi**: Contains the API controllers and endpoints.
2. **Ambev.DeveloperEvaluation.Domain**: Defines the core domain entities and business rules.
3. **Ambev.DeveloperEvaluation.Application**: Implements application services and use cases.
4. **Ambev.DeveloperEvaluation.ORM**: Configures database mappings and EF Core context.
5. **Ambev.DeveloperEvaluation.Common**: Provides shared utilities, validation, and error handling.
6. **Ambev.DeveloperEvaluation.Unit**: Contains unit tests for domain and application logic.
7. **Ambev.DeveloperEvaluation.Integration**: Contains integration tests for database and API interactions.

## Setup Instructions
Follow these steps to configure and run the solution:

### Prerequisites
- .NET 8 SDK
- Postgres (optional, for persistent database testing)

### Configuration
1. Clone the repository:
```
git clone <repository-url> cd <repository-folder>
```
   
2. Restore dependencies:
```
dotnet restore
```


3. Configure the database connection string in `appsettings.json` (for Postgres):   
```
"ConnectionStrings": { "DefaultConnection": "Server=localhost;Database=DeveloperEvaluation;Trusted_Connection=True;" }
```


4. Apply migrations (if using Postgres):   
```
dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM
```


### Running the Application
1. Start the API:
```
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```
2. Access the Swagger UI for API documentation:
```
http://localhost:5000/swagger
```

### Running Tests
1. Run unit tests:
```
dotnet test tests/Ambev.DeveloperEvaluation.Integration
dotnet test tests/Ambev.DeveloperEvaluation.Unit
dotnet test tests/Ambev.DeveloperEvaluation.Functional
```


## API Usage
### Endpoints
- **Sales**: `/api/sales`
- **Products**: `/api/products`
- **Carts**: `/api/carts`
- **Users**: `/api/users`

### Query Parameters
- **Pagination**: `_page` and `_size`
- **Ordering**: `_order`
- **Filtering**: Field-based filters with support for ranges and partial matches.

### Example Requests
1. Get paginated sales:
```
GET /api/sales?_page=1&_size=10
```

2. Filter products by category:
```
GET /api/products?category=electronics
```
3. Order sales by date:
```
GET /api/sales?_order="date desc"
```

## Error Handling
The API returns structured error responses with the following format:
```
{ "type": "string", "error": "string", "detail": "string" }
```
### Example Errors
1. **Validation Error**:
```
{ "type": "ResourceNotFound", "error": "Sale not found", "detail": "The sale with ID 12345 does not exist" }
```

## Business Rules
1. **Discount Tiers**:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount
2. **Restrictions**:
   - Maximum 20 items per product
   - No discounts for quantities below 4 items

## Event Logging
The following events are logged:
- `SaleCreated`
- `SaleModified`
- `SaleCancelled`
- `ItemCancelled`

## Contribution Guidelines
1. Follow Git Flow for branching:
   - `feature/` for new features
   - `bugfix/` for fixes
   - `release/` for releases
2. Write meaningful commit messages.
3. Ensure all tests pass before submitting a pull request.

## License
This project is licensed under the MIT License. See the LICENSE file for details.
