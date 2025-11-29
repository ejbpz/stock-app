# Stocks App

A robust and cleanly architected web application built with ASP.NET Core 8 and Razor Views, designed to consume real-time and historical market data through the Finnhub API. This project demonstrates solid software engineering practices for building maintainable and production-ready web applications in .NET.

---

## 🎯 Project Goals
This application focuses on demonstrating practical skills in:

- Clean Architecture design in ASP.NET Core
- MVC pattern with Razor Views
- API consumption and data integration using HttpClient
- UI modularization with View Components and Tag Helpers
- Data validation, Filters, Logging, and Dependency Injection (DI)
- Automated testing with xUnit
- Working with external data providers (Finnhub)

---

## 🧪 Features

✔️ Real-time and historical stock data from Finnhub  
✔️ Search for stock symbols and view detailed market information  
✔️ Interactive and dynamic UI with Razor + Tailwind  
✔️ Clean Architecture with clear separation of concerns  
✔️ View Components for reusable market widgets  
✔️ Custom Tag Helpers for UI consistency  
✔️ Server-side validation and Filters  
✔️ Logging using Serilog  
✔️ Full test coverage of core components using xUnit  
✔️ Responsive UI with **Tailwind 4**  

---

## 🧰 Technologies Used

- **.NET 8** (ASP.NET Core MVC + Razor Views)  
- **Finnhub API** (external financial data provider)  
- **Entity Framework Core**  
- **xUnit** (unit testing)  
- Clean Architecture  
- HttpClient & typed services  
- Dependency Injection  
- Logging & Filters  
- **Tailwind 4**  
- **SQL Server**  

---

## 🛠️ Getting Started

### Prerequisites
- Visual Studio 2022
- .NET 8 SDK
- Finnhub API Key  
- SQL Server 

### Installation

1. Clone the repository:
```bash
git clone https://github.com/ejbpz/stock-app.git
```
2. Navigate to the project directory:
```bash
cd stock-app
```
3. Restore packages:
```bash
dotnet restore
```
4. Apply migrations and update the database:
```bash
dotnet ef database update
```
5. Run the application:
```bash
dotnet run
```

---

## 📜 License
This project is open-source. Feel free to use, modify, or improve it in personal or commercial environments.
