# StockManagement

StockManagement is a minimal C# ASP.NET Core Web API project for managing product inventory. It provides basic CRUD operations and a stock adjustment endpoint, with in-memory data storage for easy setup and testing. The project also includes Swagger UI for API exploration and an audit logger for tracking changes.

## Features

- CRUD operations for products (create, read, update, delete)
- Adjust product quantity via a dedicated endpoint
- In-memory database for quick setup (EF Core InMemory)
- Audit logging of raw HTTP request bodies
- Swagger UI for API documentation and testing

## Endpoints

| Method | Route                   | Description                        |
|--------|-------------------------|------------------------------------|
| GET    | /api/products           | Get all products                   |
| GET    | /api/products/{id}      | Get a product by ID                |
| POST   | /api/products           | Create a new product               |
| PUT    | /api/products/{id}      | Update an existing product         |
| DELETE | /api/products/{id}      | Delete a product                   |
| POST   | /api/products/{id}/adjust | Adjust product quantity by delta |

### Example: Adjust Quantity

`POST /api/products/1/adjust`
```json
{
  "quantityChange": 5
}
```

## Setup

1. **.NET 8 SDK required**  
   Download from https://dotnet.microsoft.com/download

2. **Restore and build**
   ```
   dotnet restore
   dotnet build
   ```

3. **Run the API**
   ```
   dotnet run --project StockManagement
   ```

4. **Explore with Swagger UI**  
   Visit [http://localhost:5000/swagger](http://localhost:5000/swagger) (or the port shown in the console).

## Notes

- Data is stored in-memory and will be lost when the app stops.
- Audit logs are written to `audit.log` in the project directory.
- The project is ready for extension to use a real database by updating the EF Core provider and connection string.
