# RESTful web services

A simple task to create and update tasks.
Contains ASP.NET Core Web Api and Xunit test projects.

## Database initialize

1. Create new SQL database named "taskDb".
2. Execute sql scripts from file -> SqlScripts.sql
in order to create table and stored procedures.

## Usage

Two endpoints for creating and updating the task.

https://localhost:7108/api/Task - http method POST

https://localhost:7108/api/Task?{taskId} - http method PUT

Test api through swagger on project startup.