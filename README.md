Assignment_Sa (SA Tech & Consultancy) <br />

Project Overview <br />

This is a simple ASP.NET Core with MVC pattern application to manage sales transactions with master-detail entries.
The project includes authentication, dynamic product entry by selected customer, Excel and PDF exports, validations, stock deduction, global error handling and role-based access.

Database seeding with: <br />
An Admin user (UserName: ```admin```  and Password: ```123456```) <br />
Sample Customers <br />
Sample Products <br />

1. Setup Instructions: <br />
  ```git clone <your-repo-url>``` <br />

2. Update connection string in appsettings.json to connecting the database. <br />

3. ```Add-Migration InitialCreate```  (For Visual Studio) <br />
   ```Update-Database```              (For Visual Studio) <br />

3. ```dotnet ef migrations add InitialCreate```   (Visual Studio Code) <br />
   ```Update-Database ```                        (Visual Studio Code) <br />

Now Run. <br />

Notes: <br />
Admin user is seeded automatically: <br />
Username: ```admin``` <br />
Password: ```123456``` <br />

Technologies Used: <br />
ASP.NET Core MVC <br />
Entity Framework Core <br />
SQL Server <br />
Bootstrap 5 <br />
ClosedXML (Excel Export) <br />
QuestPDF (PDF Generation) <br />
JavaScript (Dynamic Form) <br />



