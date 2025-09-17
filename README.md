Assignment_Sa (SA Tech & Consultancy)

Project Overview

This is a simple ASP.NET Core with MVC pattern application to manage sales transactions with master-detail entries.
The project includes authentication, dynamic product entry by selected customer, Excel and PDF exports, validations, stock deduction, global error handling and role-based access.

Database seeding with:
An Admin user (UserName: admin  and Password: 123456)
Sample Customers
Sample Products

1. Setup Instructions:
  git clone <your-repo-url>

2. Update connection string in appsettings.json to connecting the database.

3. Add-Migration InitialCreate  (For Visual Studio)
   Update-Database              (For Visual Studio)

3. dotnet ef migrations add InitialCreate   (Visual Studio Code)
   Update-Database                          (Visual Studio Code)

4. Then Run.

Notes:
Admin user is seeded automatically:
Username: admin
Password: 123456

Technologies Used:
ASP.NET Core MVC
Entity Framework Core
SQL Server
Bootstrap 5
ClosedXML (Excel Export)
QuestPDF (PDF Generation)
JavaScript (Dynamic Form)



