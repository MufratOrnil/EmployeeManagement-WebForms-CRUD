# Employee Management – ASP.NET Web Forms CRUD

A simple, job-ready **Employee Management System** built with **ASP.NET Web Forms**, **C#**, and **SQL Server**. This project demonstrates classic **CRUD** (Create, Read, Update, Delete) operations using **GridView** and **ADO.NET**, similar to many legacy enterprise Web Forms applications still running in production.

## 🚀 Features

* Display employees in a responsive, Bootstrap-styled GridView
* Add employees with form validation (Name, Email required)
* Edit existing employee records
* Delete employees with a confirmation popup
* Search employees by Name (`LIKE` query)
* Secure ADO.NET operations using parameterized queries
* Centralized connection string in `Web.config`

## 🛠️ Tech Stack

* **ASP.NET Web Forms (.NET Framework 4.8)**
* **C# (code-behind, ADO.NET)**
* **Microsoft SQL Server** (Express or full)
* **Bootstrap 5** for UI styling

## 🗄️ Database Setup

1. Create a database named **EmployeeDB**
2. Run the following SQL script:

```sql
CREATE TABLE Employees (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Department NVARCHAR(50) NULL,
    JoinDate DATE NULL
);
```

## ⚙️ Configuration (Web.config)

Update your SQL Server connection string:

```xml
<connectionStrings>
    <add name="EmployeeCon"
         connectionString="Data Source=\\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

Modify `Data Source` or authentication if needed.

## ▶️ Running the Project

1. Open the solution in **Visual Studio 2019/2022**
2. Right-click **Employees.aspx** → Set as Start Page
3. Press **Ctrl + F5** to run
4. Use the UI to:

   * Add employees
   * Edit and update records
   * Delete records
   * Search by Name

## 🖼️ Screenshot

<img width="1920" height="1035" alt="Screenshot 2025-12-06 080823" src="https://github.com/user-attachments/assets/6746d53e-7e7d-494d-ab8b-e6162a72f55d" />



**Includes:**

* Search bar for filtering
* Add/Edit Employee form
* GridView listing employees
* Edit/Delete action buttons

## 🎯 Why This Project?

This project mirrors real-world ASP.NET Web Forms maintenance work. It helps you practice:

* Page lifecycle & postback behaviour
* GridView events (RowCommand, RowEditing, RowDeleting)
* ADO.NET patterns (`SqlConnection`, `SqlCommand`, `SqlDataAdapter`)
* Working with `Web.config` connection strings
* Debugging common Web Forms issues in support roles

Perfect for:

* Learning Web Forms CRUD
* ASP.NET job preparation
* Adding a practical project to your resume
* Demo for interviews

## 📄 License

This project is open-source and free to use.
