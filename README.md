# Task Planner
A simple easy-to-use standalone application to plan project road maps, user stories, and backlogs, estimates effortlessly using the appealing and user friendly interface for software development.

# Basic Requirements to run
* VS 2017 Update 15.3 and above as it uses .NET Core 2.0
* PostgreSQL 10 and above

# Changes to be done in source to setup application 
* Get a client and secret key by registering your app in google. For Google Login to work.
Reference - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?tabs=aspnetcore2x
* Once registered, replace the client and secret ID in apisettings.config file available in the source https://github.com/bharatdwarkani/taskplanner/blob/development/TaskPlanner/appsettings.json
* Also, replace your PostgreSQL DB credentials in the same file `appsettings.json`
* Now, host the application which is ready to run.
