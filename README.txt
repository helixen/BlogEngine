READ ME

Technical Requirements

Windows 7 or later
Windows Server 2012 R2 or later

.NET Core Hosting Bundle

IIS Framework version: 
.net core 3.1

minimun Database Engine: 
SQL Server 2017

Visual Studio 2019

Step by step deploy

DATA BASE
1. Create database BlogEngine.BD on Sql Server, default configuration is ok.
2. Run "6.DataBase/1_BlogEngineDB_structure.sql" script, this will create tables and relationships.
3. Run "6.DataBase/2_BlogEngineDB_data.sql" script, this will populate USERS table.

APPLICATION

4. Create BlogEngineAPI website on iis. 
5. Create an application pool that has .net clr version as "No Managed Code".
6. Verify that "IIS IUSRS" account has access to the selected folder for the website.
7. Include "IIS IUSRS/name of app pool" on the SQL SERVER security logins with data read and data write permissions.
8. Create BlogEngineAPI website on iis. 
9. Create a different application pool that has .net clr version as "No Managed Code". This is required to prevent execution colusion. 
10. Verify that "IIS IUSRS" account has access to the selected folder for the website.
11. Include "IIS IUSRS/name of he other app pool" on the SQL SERVER security logins with data read and data write permissions.

DEPLOYMENT

12. Start Visual Studio with elevated permissions. This is required to write deployment files. 

13. At BlogEngineAPI create or select a folder publish profile, select the folder that hosts BlogEngineAPI website. 
For windows iis hosting publish as:
- selfcontained
- netcoreapp 3.0
- target runtime winx-64
- verify Database location configuration: BlogEngineAPI/appsettings.json/ConnectionStrings/DefaultConnection
Databese expected name is [BlogEngine.BD]

14. Save and publish

15. At BlogEngine set Api location configuration at
"BlogEngine/appsettings.json/ApiUrl" using BlogEngineAPI websit url.

16. At BlogEngine create or select a folder publish profile, select the folder that hosts BlogEngine website. 
For windows iis hosting publish as:
- selfcontained
- netcoreapp 3.0
- target runtime winx-64 

17. Save and publish

18. Open BlogEngine url on a browser.

AUTHENTICATION CREDENTIALS

(You can see this information at signin screen, for testing purpose only)
Writer: user:fredm, password:123456
Editor: user:joewk, password:654321

TESTING
Impoort "7.Testing BlogEngine.postman_collection.json" on Postman to test API.

TOTAL DEVELOPMENT EFFORT 21.26 hours
Greatest challenge Dependency injection



