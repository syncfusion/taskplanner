# Task Planner
Task Planner -  A simple easy to use application to create project roadmaps, user stories, project estimates effortlessly using easy to use interface. Created project can easily be shared to any one by adding the receipents email id.

# Basic Requirements to run
VS2017 update 15.3 and above as it uses .net core 2.0
MS SQL Server for hosting SQL Database (Sample DB can be found here and can be hosted for getting started) https://github.com/bharatdwarkani/taskplanner/tree/development/Database%20sample 

# Changes need to be done in source to setup application 
Get a client and secret key by registering you app in google. For Login to work.
Reference - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?tabs=aspnetcore2x
Once registered replace client and secret id in apisettings.config file availabe in source https://github.com/bharatdwarkani/taskplanner/blob/development/TaskPlanner/appsettings.json
Also replace sql server db credentials after hosting database in same file appsettings.json
Now host application and it will be ready to run.

# Third Party libraries credits

Bootstrap
Toastr
typescript
Systemjs
Syncfusion ej2 components (grid, button , dialog) - http://ej2.syncfusion.com 



