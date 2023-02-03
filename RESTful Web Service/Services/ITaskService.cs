using RESTful_Web_Service.Entities;
using RESTful_Web_Service.Models;

namespace RESTful_Web_Service.Services
{
    public interface ITaskService
    {
        Task<bool> CreateTask(MyTask task);
        Task<bool> IsModelValid(TaskModel model);
        Task<bool> UpdateTask(MyTask task);
    }
}