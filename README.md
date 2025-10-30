# Logistics Tracking System

A web-based logistics tracking system to manage parcels, routes, drivers, and delivery statuses. This system is built with:

* **Backend**: ASP.NET Core Web API (C#)
* **Database**: Microsoft SQL Server (MSSQL)
* **Frontend**: HTML/CSS/JS (Vanilla)
* **Deployment**: Local or Cloud (Azure recommended)

---

## Features

* Track parcel status and routes
* Assign drivers to routes
* View parcel history
* Admin panel for managing warehouses, drivers, and parcels
* Simple HTML/CSS/JS frontend for easy preview

---

## Prerequisites

Before running the project, ensure you have:

* [Visual Studio 2022](https://visualstudio.microsoft.com/) or later (with ASP.NET and Web Development workload)
* [SQL Server Express / Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* Optional: [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli) (if deploying to Azure)

---

## Setup Database (MSSQL)

1. **Install SQL Server**
   Download and install SQL Server Developer/Express edition. Make sure **SQL Server Management Studio (SSMS)** is installed.

2. **Create a new database**
   Open SSMS → Connect to your SQL Server → Right-click `Databases` → `New Database...` → Name it `LogisticsDB`.

3. **Run SQL Scripts**

   * Navigate to `database/` folder (if available in repo)
   * Run the `.sql` files in SSMS to create tables and seed initial data.
   * Alternatively, you can manually create tables according to the models in the project.

4. **Update Connection String**
   In the backend project (`Web.config`), update the connection string:

   ```xml
   <connectionStrings>
        <add name="LogisticsDBConnectionString" connectionString="Data Source=xxxxxxxx Catalog=LogisticsDB;Integrated Security=True;TrustServerCertificate=True"
            providerName="System.Data.SqlClient" />
    </connectionStrings>
   ```

---

## Setup Backend (ASP.NET Core Web API)

1. Open `LogisticsTrackingSystem.sln` in Visual Studio.

2. Restore NuGet packages:
   `Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution -> Restore`

3. Build the solution:
   `Build -> Build Solution`

4. Run the backend:

   * Press `F5` (Debug) or `Ctrl + F5` (Run without Debug)
---

## Deployment

* **Local deployment**: Use Visual Studio + SQL Server on your machine.
* **Azure deployment**:

  1. Create an Azure App Service for ASP.NET Core.
  2. Create an Azure SQL Database.
  3. Update the connection string to use Azure SQL.
  4. Publish the project from Visual Studio (`Right-click Project -> Publish`).

---

## Project Structure

```
logistics-tracking-system/
├─ LogisticsTrackingSystem/               # ASP.NET Core Web API
│  ├─ Services/
│  ├─ Models/
│  ├─ Views/
│  ├─ Scripts/
│  ├─ Public/
│  │  ├─ css/
│  │  └─ js/
│  └─ Web.config            
├─ demo/              # SQL scripts
│  ├─ db_logic/
│  ├─ ddl/
│  ├─ dml/
│  └─ dql/
└─ README.md
```

---

## Notes

* Default admin credentials: (if any)
* Make sure to run SQL scripts before starting the backend.
* Use Postman to test APIs before using the frontend.
