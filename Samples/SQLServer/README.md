# Northwind Sample – UWP app connecting to SQL Server

This end-to-end sample demonstrates how to connect a Universal Windows Platfornm (UWP) app to SQL Server via Desktop Bridge. It shows how to take advantage of UWP features, such as XAML UI, Telerik DataGrid, etc., while retaining the Win32 application for existing business logic. It also shows how to use an App Service to communicate between the UWP and Win32 processes. In this case, the Win32 application process manages a SQL Server connection. We assume this piece of code cannot currently migrate to UWP because there is no SqlClient API support in UWP yet.


Build/Deploy and Run the sample
-------------------------------

 - Deploy the solution by selecting **Build** \> **Deploy solution**.
 - Press F5 to run!

Notes
------

You need to install the Northwind database in a SQL Server and connect to your server from the sample app.
1.	Install SQL Express using all the defaults: https://www.microsoft.com/en-us/sql-server/sql-server-editions-express
2.	Download SQL Server Management Studio: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms
3.	Launch SSMS and connect to your computer using Windows authentication. Then from  SSMS File, Open menu select the instnwnd.sql file. It should load into the Query window. Tap the Execute button to run the script (ignore any errors about missing stored procedures which we aren’t used for this sample).
4.	Under Databases in left pane, select Northwind. Right-click and choose New Query. Type select * from Products in the Query window and tap Execute. You should see 77 rows returned.
5.	Change the connectonString in App.xaml.cs to your connection:
private string connectionString = @"Data Source=YourComputerName\SQLEXPRESS;Initial Catalog=NORTHWIND;Integrated Security=SSPI";
