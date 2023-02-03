using DateTimeExtensions;
using DateTimeExtensions.WorkingDays;
using Microsoft.AspNetCore.Connections;
using RESTful_Web_Service.Entities;
using RESTful_Web_Service.Enums;
using RESTful_Web_Service.Models;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RESTful_Web_Service.Services
{
    public class TaskService : ITaskService
    {
        private const int MaxSameUnfinishedTasks = 100;
        private readonly IDbConnection connection;
        public TaskService(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<bool> CreateTask(MyTask task)
        {
            var isSuccessful = false;
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertTask";
                command.Parameters.Clear();

                if (task != null)
                {
                    command.Parameters.Add(new SqlParameter("@name", task?.Name));
                    command.Parameters.Add(new SqlParameter("@description", task?.Description));
                    command.Parameters.Add(new SqlParameter("@dueDate", task?.DueDate));
                    command.Parameters.Add(new SqlParameter("@startDate", task?.StartDate));
                    command.Parameters.Add(new SqlParameter("@endDate", task?.EndDate));
                    command.Parameters.Add(new SqlParameter("@priority", task?.Priority?.ToString().ToLower()));
                    command.Parameters.Add(new SqlParameter("@status", task?.Status?.ToString().ToLower()));
                    var result = Convert.ToInt32(command.ExecuteScalar());
                    if (result == 0)
                    {
                        isSuccessful = true;
                    }
                }
                connection.Close();
            }
            return isSuccessful;
        }

        public async Task<bool> UpdateTask(MyTask task)
        {
            var isSuccessful = false;
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateTask";
                command.Parameters.Clear();

                if (task != null)
                {
                    command.Parameters.Add(new SqlParameter("@id", task?.Id));
                    command.Parameters.Add(new SqlParameter("@name", task?.Name));
                    command.Parameters.Add(new SqlParameter("@description", task?.Description));
                    command.Parameters.Add(new SqlParameter("@dueDate", task?.DueDate));
                    command.Parameters.Add(new SqlParameter("@startDate", task?.StartDate));
                    command.Parameters.Add(new SqlParameter("@endDate", task?.EndDate));
                    command.Parameters.Add(new SqlParameter("@priority", task?.Priority?.ToString().ToLower()));
                    command.Parameters.Add(new SqlParameter("@status", task?.Status?.ToString().ToLower()));
                    var result = Convert.ToInt32(command.ExecuteScalar());
                    if (result == 0)
                    {
                        isSuccessful = true;
                    }
                }
                connection.Close();
            }
            return isSuccessful;
        }
        private async Task<int> GetUnfinishedTasksCount()
        {
            using (var command = (SqlCommand)connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetUnfinishedTasks";
                command.Parameters.Clear();

                var returnParameter = command.Parameters.Add("@ReturnCount", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                await command.ExecuteScalarAsync();
                var result = Convert.ToInt32(returnParameter.Value);
                connection.Close();
                return result;
            }
        }

        public async Task<bool> IsModelValid(TaskModel model)
        {
            if (model != null)
            {
                if (model.DueDate < DateTime.UtcNow ||
                    model.DueDate.Value.DayOfWeek == DayOfWeek.Sunday ||
                    model.DueDate.Value.DayOfWeek == DayOfWeek.Saturday ||
                    model.DueDate.Value.IsHoliday(new WorkingDayCultureInfo("us")))
                {
                    return false;
                }
                if (model?.Status != null && !Enum.IsDefined(typeof(Status), model?.Status))
                {
                    return false;
                }
                if (model?.Priority != null)
                {
                    if (Enum.TryParse(typeof(Priority), model?.Priority, out var priority))
                    {
                        if (model?.Priority?.ToLower() == Priority.high.ToString().ToLower())
                        {
                            var unfinishedTasks = await GetUnfinishedTasksCount();
                            if (unfinishedTasks >= MaxSameUnfinishedTasks)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}


