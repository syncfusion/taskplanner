# Task Planner
Task Planner -  A simple easy to use application to create project roadmaps, user stories, project estimates effortlessly using easy to use interface. Created project can easily be shared to any one by adding the receipents email id.

# Basic Requirements to run
* VS2017 update 15.3 and above as it uses .net core 2.0
* MS SQL Server for hosting SQL Database (Sample DB can be found here and can be hosted for getting started) https://github.com/bharatdwarkani/taskplanner/tree/development/Database%20sample 

# Changes need to be done in source to setup application 
* Get a client and secret key by registering you app in google. For Login to work.
Reference - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?tabs=aspnetcore2x
* Once registered replace client and secret id in apisettings.config file availabe in source https://github.com/bharatdwarkani/taskplanner/blob/development/TaskPlanner/appsettings.json
* Also replace sql server db credentials after hosting database in same file appsettings.json
* Now host application and it will be ready to run.

# Key Features

* Login using google account
* Share projects to any one by using there gmail id 
* Create, edit, delete and share projects
* Effortlessly create road map ,user stories as if you are typing in an doc editor in a grid.
Categorize and group stories by theme, epics in a grid view
* Add estimates, penalty, benefit, priority, theme, epic, sprint, release, assignee and priority to a story.
* Export stories to excel
* Developed using cutting edge technology .net core -  cross platform compatible can be hosted on linux / windows.
* Replaces email / excel and organizes features list at one place

* Note: This isn't an actual issue tracker but it helps in planning stories to be added in backlog of issue tracker its a first stage of your development cycle  highly usefull for startup companies, consultancy team , product owners, scrum masters for writing user stories / feature plan effortlessly.

# Use Cases

* Start-up companies can use this to prepare road map for their products.
* Consulting companies can use this to prepare a plan or estimate cost / hour for requirements and can share to customer.
* Freelancers or small teams can use this to prepare a list of features for their product.
* Product Owners / Scrum master can use this to create a plan for their product release or sprints.
* Product Owners / Scrum master can use this to prepare and share road maps.
* As it provides organized for story writing effortlessly can be used as a replacement for document editors / excel sheets.
* There are several fields which can be used in different ways based on needs either for preparing road maps, sprint plan, release plan, preparing estimates.
* Stories can be exported to excel.
* Stories can be grouped / sorted by themes, epics, estimation, assignee, sprint / release easily.
* Estimate, penalty, benefit & priority can also be set for stories.
* Reduces time and effort to write stories by product owner as they are not writing stories in actual issue tracker, which takes time for a single-story creation, who can then pass on written stories to scrum masters to convert to actual tasks with detailed requirement in issue tracker.
* Can be used as an initial stage tool in software development life cycle for filtering features and setting priority.
* Can also be used as an application to create check lists. 
* Share testing plan to testing engineers.



# Future Enhancements

* Import from Excel, CSV, Jira XML.
* Kanban boards for viewing stories by sprint, release, theme, epic
* Chart & Graph visualisation
* Count based Statistics
* Email to user when project is shared
* Retaing selected columns for a project in database so that state of selected columns are retained project wise.
* Integration with issue trackers JIRA, Trello, GitHub, GitLab.
* Improve Responsiveness to mobile
* Documents and screenshots maintenance 


# Third Party libraries credits

* Bootstrap, 
* Toastr, 
* typescript, 
* Systemjs, 
* Syncfusion ej2 components (grid, button , dialog) - http://ej2.syncfusion.com 


