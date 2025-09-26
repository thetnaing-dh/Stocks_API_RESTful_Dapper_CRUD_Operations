# Stocks API - RESTful CRUD Operations
A RESTful API for managing stock inventory built with ASP.NET Core, Dapper, and SQL Server. This API provides full CRUD (Create, Read, Update, Delete) operations for stock items with soft delete functionality.

## Features
* GET /api/stocks - Retrieve all active stock items
* GET /api/stocks/{id} - Retrieve a specific stock item by ID
* POST /api/stocks - Create a new stock item
* PUT /api/stocks/{id} - Update an existing stock item
* DELETE /api/stocks/{id} - Soft delete a stock item (sets deleteFlag to 1)

## Technology Stack
* Framework: ASP.NET Core
* ORM: Dapper
* Database: SQL Server
* Architecture: REST API

## Models
### StockReqModel (Request Model)
* Code (string)
* Description (string)
* PurchasePrice (decimal)
* SalePrice (decimal)

### StockResModel (Response Model)
* Id (int)
* Code (string)
* Description (string)
* PurchasePrice (decimal)
* SalePrice (decimal)

## API Endpoints
### 1. Get All Stocks
        http
        GET /api/stocks
Returns all active stock items (where deleteFlag = 0)

Response:

        json
        [
            {
                "code": "STK001",
                "description": "Product A",
                "purchasePrice": 1000,
                "salePrice": 1500
            }
        ]
### 2. Get Stock by ID
        http
        GET /api/stocks/{id}
Returns a specific stock item by ID

Response:

        json
        {
            "code": "STK001",
            "description": "Product A",
            "purchasePrice": 10.50,
            "salePrice": 15.99
        }

### 3. Create Stock
        http
        POST /api/stocks
Creates a new stock item

Request Body:

        json
        {
            "code": "STK002",
            "description": "Product B",
            "purchasePrice": 1000,
            "salePrice": 1200
        }

Response:
* Success: 200 OK with "Created Successfully"
* Failure: 400 Bad Request with "Creation Failed"

### 4. Update Stock
http
PUT /api/stocks/{id}
Updates an existing stock item

Request Body:

        json
        {
            "code": "STK002",
            "description": "Updated Product",
            "purchasePrice": 1500,
            "salePrice": 2000
        }

Response:
* Success: 200 OK with "Updated Successfully"
* Failure: 400 Bad Request with "Updation Failed"
* Not Found: 404 Not Found with "Stock Not Found"

### 5. Delete Stock
        http
        DELETE /api/stocks/{id}

Soft deletes a stock item (sets deleteFlag to 1)

Response:
* Success: 200 OK with "Deleted Successfully"
* Failure: 400 Bad Request with "Deletion Failed"
* Not Found: 404 Not Found with "Stock Not Found"

Setup Instructions
## Prerequisites
* .NET 6.0 or later
* SQL Server
* Dapper package
* Microsoft.AspNetCore.Mvc package

## Installation
1. Clone the repository

        bash
        git clone https://github.com/thetnaing-dh/Stocks_API_RESTful_Dapper_CRUD_Operations/tree/master
        cd RestAPI_Dapper_CRUD

2. Database Setup

        sql
        CREATE DATABASE StockDB;
        GO
        
        USE StockDB;
        GO
        
        CREATE TABLE Tbl_Stock (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Code NVARCHAR(50) NOT NULL,
            Description NVARCHAR(255),
            PurchasePrice DECIMAL(18,2),
            SalePrice DECIMAL(18,2),
            deleteFlag BIT DEFAULT 0
        );

3. Update Connection String
Modify the _connectionString variable in StocksController.cs with your SQL Server credentials:

        csharp
        private readonly string _connectionString = "Server=.;Database=StockDB;User Id=sa;Password=your_password;TrustServerCertificate=True;";

4. Install Dependencies

        bash
        dotnet add package Dapper
        dotnet add package Microsoft.AspNetCore.Mvc

5.Run the Application

        bash
        dotnet run

## Error Handling
The API includes comprehensive error handling:
* 404 Not Found: When requested stock item doesn't exist
* 400 Bad Request: When CRUD operations fail
* 200 OK: When operations succeed

## Future Enhancements
* Add authentication and authorization
* Implement pagination for GET all endpoint
* Add search and filtering capabilities
* Implement proper logging
* Add unit tests
* Implement repository pattern for better separation of concerns

## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License
This project is for educational purpose.
