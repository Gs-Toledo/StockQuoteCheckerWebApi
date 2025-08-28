# Stock Quote Checker WebApi

[![.NET 8](https://img.shields.io/badge/.NET-8-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)

## 1. Project Overview

This project is a .NET 8 Web API. The API integrates with the Yahoo Finance external service to fetch stock quotes. Its core purpose is to retrieve the latest price for a given stock ticker, persist this information into a local database to build a historical record, and expose this data through a RESTful endpoint.

## 2. Technologies Used

-   **Framework:** .NET 8
-   **API:** ASP.NET Core Web API
-   **Database:** SQL Server (via LocalDB)
-   **ORM:** Entity Framework Core 9
-   **Primary Language:** C#

## 3. Setup Instructions

Follow these steps to get the application running on your local machine.

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   SQL Server Express LocalDB (installed with Visual Studio's "ASP.NET and web development" workload).
-   An IDE like Visual Studio 2022, JetBrains Rider, or VS Code.

### Step 1: Clone the Repository

Clone this repository to your local machine:
```bash
git clone [https://github.com/Gs-Toledo/StockQuoteCheckerWebApi.git](https://github.com/Gs-Toledo/StockQuoteCheckerWebApi.git)
cd StockQuoteCheckerWebApi
```

### Step 2: Configure the Database Connection

The application is configured to use SQL Server Express LocalDB by default. The connection string can be found in `appsettings.json`. No changes are required if you have LocalDB installed.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StockMonitorDb;Trusted_Connection=True;"
}
```

### Step 3: Apply EF Core Migrations

This command will create the `StockMonitorDb` database and its tables based on the Entity Framework Core models.

Open a terminal in the project's root directory and run:
```bash
dotnet ef database update
```

### Step 4: Run the Application

Run the application using the .NET CLI:
```bash
dotnet run
```
The API will start, and you can see the logs in your terminal. It will be listening on `https://localhost:xxxx` and `http://localhost:yyyy`.

### Step 5: Access the API Documentation

Once the application is running, open your web browser and navigate to the Swagger UI to explore and test the endpoints:

**`https://localhost:xxxx/swagger`**

(Replace `xxxx` with the HTTPS port number shown in the terminal output).

## 4. API Endpoints

The following endpoints are available:

### Get Latest Stock Quote

-   **Method:** `GET`
-   **Path:** `/api/stocks/{ticker}`
-   **Description:** Fetches the latest market price for a given stock ticker from the external API and saves it to the local database.
-   **Parameters:**
    -   `ticker` (string, required): The stock symbol. For Brazilian stocks, use the `.SA` suffix.
-   **Example Usage:**
    ```
    GET /api/stocks/PETR4.SA
    ```
-   **Success Response (200 OK):**
    ```json
    {
      "symbol": "PETR4.SA",
      "regularMarketPrice": 40.25,
      "RegularMarketTime": "2025-08-25T13:00:00.1234567Z"
    }
    ```

### Get Stock Price History

-   **Method:** `GET`
-   **Path:** `/api/stocks/{ticker}/history`
-   **Description:** Retrieves the historical record of prices for a given ticker that has been saved in the local database.
-   **Parameters:**
    -   `ticker` (string, required): The stock symbol to retrieve the history for.
-   **Example Usage:**
    ```
    GET /api/stocks/PETR4.SA/history
    ```
-   **Success Response (200 OK):**
    ```json
    [
      {
        "id": 1,
        "ticker": "PETR4.SA",
        "price": 40.25,
        "timestamp": "2025-08-28T16:30:00Z"
      },
      {
        "id": 2,
        "ticker": "PETR4.SA",
        "price": 40.28,
        "timestamp": "2025-08-28T16:35:00Z"
      }
    ]
    ```

## 5. Third-Party API Documentation

This project integrates with the **Yahoo Finance API**. This is an unofficial API, and as such, it does not have formal public documentation provided by Yahoo. The endpoints used were based on community-documented resources.

-   A helpful resource describing the API can be found here: [Unofficial Yahoo Finance API Documentation]([https://www.yahoofinanceapi.com/blog/yahoo-finance-api-basics](https://www.reddit.com/r/sheets/comments/12snqft/broken_yahoo_finance_api_url/?tl=pt-br)).
