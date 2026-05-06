# Pusula.UI

## 📌 Overview

Pusula.UI is an ASP.NET Core MVC frontend application for a modular employee management system.

It works together with **Pusula.API**, which provides RESTful backend services for authentication, users, permissions, and worksite management.

---

## 🧱 Architecture

- **Frontend:** ASP.NET Core MVC (Razor Views)
- **Backend:** ASP.NET Core Web API (Pusula.API)
- **Communication:** Service layer via HTTP (BaseApiAdapter)
- **No direct database access from UI**

---

## 🔐 Authentication

The system uses a **hybrid authentication approach**:

- Pusula.API uses **JWT Bearer authentication**
- Pusula.UI uses **Cookie-based authentication**
- After login, UI stores the JWT token inside user claims
- This token is used when calling API services

---

## 🎯 Features

- Authentication (Login / Register / Logout)
- Phone verification
- User management
- Permission management
- Worksite management
- Reference data (provinces, districts)

---

## ⚙️ Tech Stack

**Frontend**
- ASP.NET Core MVC (Razor Views)
- Bootstrap (Gentelella)
- jQuery
- DataTables
- 
**Backend**
- ASP.NET Core Web API
- Entity Framework Core
- MySQL (8.0+)
- JWT Authentication

---

## 🚀 Getting Started

### Requirements
- .NET 6+
- Running Pusula.API
- MySQL (8.0+)

### Run

```bash
dotnet restore
dotnet build
dotnet run
