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

> Demo video will be added.

* Login

  <img width="1919" height="1019" alt="image" src="https://github.com/user-attachments/assets/640d76d3-0b98-4610-badb-e4d0825b0cc4" />

* Dashboard view
  
  (Admin)
  
  <img width="1919" height="912" alt="image" src="https://github.com/user-attachments/assets/0d830a84-a04c-4b0a-9580-1a5adcc5ae5d" />

  <img width="1919" height="1017" alt="image" src="https://github.com/user-attachments/assets/e4675530-6584-4612-881a-34fc7e34d0a9" />
  
  (User)
  
  <img width="1915" height="1016" alt="image" src="https://github.com/user-attachments/assets/59b28903-6ea9-4f0a-bb82-ea0cb7be04ee" />


* Stock transactions
  
  <img width="1919" height="1021" alt="image" src="https://github.com/user-attachments/assets/bbe8ae73-2279-4e38-bb47-64923f18c83c" />
  <img width="1919" height="1018" alt="image" src="https://github.com/user-attachments/assets/3682d567-3b4c-4b6c-80ca-844b60c67633" />

* Categories
  
  (Admin)
  
  <img width="1918" height="1017" alt="image" src="https://github.com/user-attachments/assets/daf7fcc1-0abd-4484-baa0-55208001bbc9" />
  
  <img width="1919" height="1016" alt="image" src="https://github.com/user-attachments/assets/f3df818d-0eec-4cd3-874e-80e7b93294ab" />

  <img width="1919" height="1017" alt="image" src="https://github.com/user-attachments/assets/47b8053a-1a7f-40bb-8500-219de50827fb" />

  (User)

  <img width="1918" height="1016" alt="image" src="https://github.com/user-attachments/assets/19e018ac-c787-4cb5-b685-3450de14fcae" />

  
* Products
  
  (Admin)

  <img width="1915" height="1020" alt="image" src="https://github.com/user-attachments/assets/101e9f44-e479-4e95-9c85-ef15934fcdd2" />

  <img width="1918" height="1021" alt="image" src="https://github.com/user-attachments/assets/43f96e50-7869-498d-a256-32fd25e54dcd" />

  <img width="1919" height="1016" alt="image" src="https://github.com/user-attachments/assets/4b604235-6589-4ce2-959f-da51e6d65333" />

  (User)

  <img width="1918" height="1009" alt="image" src="https://github.com/user-attachments/assets/88ff50dc-c68e-4fb1-aed7-0f6214097f43" />


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
