Installation procedure:
Visual Studio Local DB or SQL Server instance is required.
1.	Create new database with name Demo (or with another name)
2.	Run on this database script.sql
3.	Optionally change connection string in Projects\Demo.WebApi\Web.config
4.	Open solution in Visual Studio and run two apps: Demo.WebApi and Demo.WebApp

By default, Demo.WebApi runs http://localhost:2776/ and Demo.WebApp runs on http://localhost:54992/. When port change on Demo.WebApi modification in Projects\Demo.WebApp\Scripts\App\CustomerViewModel.js to correctly setup the port.
