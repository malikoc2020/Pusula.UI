# Pusula.UI

## 📌 Overview

Pusula.UI is an ASP.NET Core MVC frontend application that consumes RESTful services provided by Pusula.API to manage employees, permissions, and worksite operations in a modular system.

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
- AJAX (HTTP requests for API communication) 


## 🚀 Getting Started

### Requirements
- .NET 7
- Running Pusula.API
- MySQL (8.0+)

### Run

1. Clone the repository

```bash
git clone https://github.com/malikoc2020/Pusula.UI.git
```

2. Open the solution file in **Visual Studio**

3. Set startup project as `Pusula.UI`

4. Configure ApiSettings:BaseUrl in appsettings.json
   
5. Run the project using IIS Express or `Ctrl + F5`
