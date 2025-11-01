# Logistics Tracking System

A web-based logistics tracking system to manage parcels, routes, drivers, and delivery statuses. This system is built with:

- **Backend**: ASP.NET Core Web API (C#)
- **Database**: Microsoft SQL Server (MSSQL)
- **Frontend**: HTML/CSS/JS (Vanilla)
- **Deployment**: Local or Cloud (Azure recommended)

---

## Features

- Track parcel status and routes
- Assign drivers to routes
- View parcel history
- Admin panel for managing warehouses, drivers, and parcels
- Simple HTML/CSS/JS frontend for easy preview

---

## Prerequisites

Before running the project, ensure you have:

- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later (with ASP.NET and Web Development workload)
- [SQL Server Express / Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- Optional: [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli) (if deploying to Azure)

---

## Setup Database (MSSQL)

1. **Install SQL Server**
   Download and install SQL Server Developer/Express edition. Make sure **SQL Server Management Studio (SSMS)** is installed.

2. **Create a new database**
   Open SSMS → Connect to your SQL Server → Right-click `Databases` → `New Database...` → Name it `LogisticsDB`.

3. **Run SQL Scripts**

   - Navigate to `database/` folder (if available in repo)
   - Run the `.sql` files in SSMS to create tables and seed initial data.
   - Alternatively, you can manually create tables according to the models in the project.

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

   - Press `F5` (Debug) or `Ctrl + F5` (Run without Debug)

---

## Project Structure

```
logistics-tracking-system/
├─ LogisticsTrackingSystem/               # ASP.NET Web API
│  ├─ Services/
│  ├─ Models/
│  ├─ Views/
│  ├─ Scripts/
│  ├─ Public/
│  │  ├─ css/
│  │  └─ js/
│  └─ Web.config
├─ demo/                                  # SQL scripts
│  ├─ db_logic/
│  ├─ ddl/
│  ├─ dml/
│  └─ dql/
└─ README.md
```

---

## Endpoints

### Frontend

| Page Path                       | Role       | Description                           |
| ------------------------------- | ---------- | ------------------------------------- |
| **Public Pages**                |            |                                       |
| `/Views/sign-in.html`           | Public     | Employee login page                   |
| `/Views/sign-up.html`           | Public     | Employee registration page            |
| `/Views/tracking.html`          | Public     | Parcel tracking page for customers    |
| **Driver Pages**                | `driver`   |                                       |
| `/Views/driver/tracking.html`   | Driver     | View assigned parcels & tracking info |
| **Employee Pages**              | `employee` |                                       |
| `/Views/employee/add.html`      | Employee   | Add new parcel or delivery request    |
| `/Views/employee/parcel.html`   | Employee   | Manage parcel list / update status    |
| **Manager Pages**               | `manager`  |                                       |
| `/Views/manager/dashboard.html` | Manager    | Admin dashboard / system overview     |
| `/Views/manager/customer.html`  | Manager    | Manage customer data                  |
| `/Views/manager/employee.html`  | Manager    | Manage employee data & roles          |
| `/Views/manager/location.html`  | Manager    | Manage delivery locations             |
| `/Views/manager/parcel.html`    | Manager    | View / manage all parcels in system   |
| `/Views/manager/route.html`     | Manager    | Manage delivery routes                |
| `/Views/manager/vehicle.html`   | Manager    | Manage vehicle information            |

---

### Backend

Base URL: `/Service/DbService.svc/`

| Endpoints                      | Method | Description                         |
| ------------------------------ | ------ | ----------------------------------- |
| **Authentication**             |        |                                     |
| `/signin`                      | POST   | Sign in employee account            |
| `/signup`                      | POST   | Register new employee account       |
| **Customers**                  |        |                                     |
| `/customers`                   | GET    | Get all customers                   |
| `/customers/{phone}`           | GET    | Get customer by phone               |
| `/customers`                   | POST   | Insert new customer                 |
| `/customers`                   | PUT    | Update customer                     |
| `/customers/{id}`              | DELETE | Delete customer by ID               |
| **Employees**                  |        |                                     |
| `/employees`                   | GET    | Get all employees                   |
| `/employees/id/{id}`           | GET    | Get employee by ID                  |
| `/employees/role/{role}`       | GET    | Get employee by role                |
| `/employees`                   | POST   | Insert new employee                 |
| `/employees`                   | PUT    | Update employee                     |
| `/employees/{id}`              | DELETE | Delete employee by ID               |
| **Parcels**                    |        |                                     |
| `/parcels`                     | GET    | Get all parcels                     |
| `/parcels/stat`                | GET    | Get parcel statistics               |
| `/parcels/{id}`                | GET    | Get parcel by ID                    |
| `/parcels`                     | POST   | Insert new parcel (with details)    |
| `/parcels`                     | PUT    | Update parcel info                  |
| **Parcel Logs**                |        |                                     |
| `/parcel/logs`                 | GET    | Get all parcel logs                 |
| `/tracking/{id}`               | GET    | Get tracking logs by parcel ID      |
| `/parcel/logs`                 | POST   | Insert new parcel log               |
| **Parcel Routes**              |        |                                     |
| `/parcel/routes/{id}`          | GET    | Get parcel routes by parcel ID      |
| `/parcel/routes/driver/{id}`   | GET    | Get parcel routes by driver ID      |
| `/parcel/routes`               | POST   | Insert new parcel route             |
| **Routes**                     |        |                                     |
| `/routes`                      | GET    | Get all routes                      |
| `/routes/{id}`                 | GET    | Get route by ID                     |
| `/routes`                      | POST   | Insert new route                    |
| `/routes`                      | PUT    | Update route                        |
| `/routes/{id}`                 | DELETE | Delete route by ID                  |
| **Vehicles**                   |        |                                     |
| `/vehicles`                    | GET    | Get all vehicles                    |
| `/vehicles/{id}`               | GET    | Get vehicle by ID                   |
| `/vehicles`                    | POST   | Insert new vehicle                  |
| `/vehicles`                    | PUT    | Update vehicle                      |
| `/vehicles/{id}`               | DELETE | Delete vehicle by ID                |
| **Locations**                  |        |                                     |
| `/locations`                   | GET    | Get all locations                   |
| `/locations/id/{id}`           | GET    | Get location by ID                  |
| `/locations/contact/{contact}` | GET    | Get locations by contact name/phone |
| `/locations`                   | POST   | Insert new location                 |
| `/locations`                   | PUT    | Update location                     |
| `/locations/{id}`              | DELETE | Delete location by ID               |

---

## Notes

- Default admin credentials: (if any)
- Make sure to run SQL scripts before starting the backend.
- Use Postman to test APIs before using the frontend.
