CREATE TABLE "myTask" (
	"id" UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	"name" VARCHAR(20),
	"description" VARCHAR(20),
	"dueDate" DATETIME,
	"startDate" DATETIME,
	"endDate" DATETIME,
	"priority" VARCHAR(20),
	"status" VARCHAR(20)
);
GO

Create procedure InsertTask  
(  
@name Varchar (20),  
@description varchar (20),  
@dueDate DATETIME,
@startDate DATETIME,
@endDate DATETIME,
@priority Varchar (20),
@status Varchar (20)
)  
as  
begin  
Insert into myTask ("name","description","dueDate","startDate","endDate","priority","status") values (@name,@description,@dueDate,@startDate,@endDate,@priority,@status)  
End  
GO

Create procedure UpdateTask  
(  
@id UNIQUEIDENTIFIER,
@name Varchar (20),  
@description varchar (20),  
@dueDate DATETIME,
@startDate DATETIME,
@endDate DATETIME,
@priority Varchar (20),
@status Varchar (20)
)  
as  
begin  
update myTask 
set "name" = @name, "description" = @description, "dueDate" = @dueDate, "startDate" = @startDate, "endDate" = @endDate, "priority" = @priority, "status" = @status
where "id" = @id
End  
GO


Create procedure GetUnfinishedTasks
as  
begin  
declare @ReturnCount int
SET NOCOUNT ON;
SET @ReturnCount = (select count(*) from myTask
where "priority" = 'high' and "status" != 'finished'
group by "dueDate")
return @ReturnCount
End  
GO
