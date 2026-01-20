# Inventory Management System (IMS)

A **Role-Based Inventory Management System** built using **ASP.NET Core 8**, **Blazor Server**, and **Clean Architecture**, designed for **portfolio presentation and freelance demonstrations**.
The system focuses on **stock tracking, role-based access control, and auditability** with a clean and scalable architecture.

---

## Project Overview

This Inventory Management System allows organizations to manage **categories, products, and stock transactions** with strict **role-based access control**.

The application maintains **real-time stock levels** and logs every stock movement to ensure transparency and accountability.

---

## Key Objectives

* Demonstrate **Clean Architecture** with real-world use cases
* Implement **Role-Based Authorization** using ASP.NET Identity
* Maintain accurate **stock in / stock out** records
* Provide a **dashboard-driven UI** for better visibility
* Serve as a **professional portfolio & freelance demo project**

---

## Architecture Overview

The project follows **Clean Architecture** principles and is implemented as a **single solution with multiple projects**:

```
IMS.sln
│
├── IMS.API             → REST APIs, Controllers, Authorization
├── IMS.Application     → Business logic, Services, Interfaces
├── IMS.Domain          → Entities, Core domain models
├── IMS.Infrastructure  → EF Core, Identity, Database access
├── IMS.Shared          → DTOs, Shared contracts
└── IMS.Web             → Blazor Server UI
```

This structure ensures:

* Loose coupling
* High testability
* Easy scalability
* Clear separation of concerns

---

## Tech Stack

### Backend

* ASP.NET Core 8
* ASP.NET Identity
* JWT Authentication
* Entity Framework Core
* SQL Server
* Database-First Approach
* REST APIs

### Frontend

* Blazor Server
* Radzen Components
* Bootstrap
* Custom State Management
* Role-based UI rendering

### Other

* Clean Architecture
* Dependency Injection
* Role-based Authorization
* Audit Trail for Stock Transactions

---

## User Roles & Permissions

### Admin

* Manage Categories (Create, Update, Delete)
* Manage Products (Create, Update, Delete)
* View Dashboard
* Stock In / Stock Out
* View Stock Transaction History

### User

* View Categories and Products
* View Dashboard
* Stock In products only

> **Note:** Both roles use the **same dashboard**, with UI and actions controlled via role-based authorization.

---

## Dashboard Features

* Category cards displaying:

  * Category name
  * Total number of products
* Stock In / Stock Out actions
* Stock transaction history table
* Role-based visibility of actions

---

## Inventory & Stock Management

* Products belong to categories
* Each product maintains a **current stock quantity**
* Stock quantity updates automatically on:

  * Stock In
  * Stock Out
* **Stock-out is blocked** if available quantity is insufficient
* Stock quantity **cannot go negative**

---

## Stock Transaction Audit Trail

Each stock transaction records:

* Date
* Transaction type (Stock In / Stock Out)
* Quantity
* Performed by (User)

This ensures full traceability and accountability.

---

## Authentication & Authorization

* ASP.NET Identity for user management
* JWT-based authentication
* Role-based authorization using `[Authorize(Roles = "...")]`
* Fixed Admin and User roles
* Login-based access (no public endpoints)

> Refresh tokens are planned for future implementation.

---

## Setup Instructions

### Prerequisites

* .NET 8 SDK
* SQL Server
* Visual Studio 2022+

### Steps

1. Clone the repository

   ```bash
   git clone https://github.com/your-username/ims.git
   ```

2. Update the database connection string in:

   * `appsettings.json`

3. Run database migrations / ensure DB exists
   (Database-First approach)

4. Run the solution:

   * Start `IMS.API`
   * Start `IMS.Web`

5. Login using Admin or User credentials

---

## Screenshots & Demo

> Screenshots and demo video will be added.

* Dashboard view
* Category cards
* Stock transactions
* Role-based UI differences

---

## Roadmap (Planned Features)

* Refresh Token implementation
* Deployment (Azure / Cloud hosting)
* Toast notifications for success/error messages
* Improved reporting & analytics
* UI/UX enhancements

---

## Contribution

This project is currently intended for **portfolio and demonstration purposes**.
Contributions, suggestions, and feedback are welcome.

---

## License

This project is licensed for **educational and demonstration use**.

---

## Author

**Ali Sufyan**
ASP.NET & Blazor Developer
Portfolio / Freelance Project
