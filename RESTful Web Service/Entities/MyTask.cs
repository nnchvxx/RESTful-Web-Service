using RESTful_Web_Service.Enums;
using RESTful_Web_Service.Models;
using System.ComponentModel.DataAnnotations;

namespace RESTful_Web_Service.Entities
{
    public class MyTask
    {
        public MyTask()
        {

        }
        public MyTask(TaskModel model)
        {
            this.Name = model?.Name ?? this.Name;
            this.Description = model?.Description ?? this.Description;
            this.DueDate = model?.DueDate ?? this.DueDate;
            this.StartDate = model?.StartDate ?? this.StartDate;
            this.EndDate = model?.EndDate ?? this.EndDate;
            this.Priority = model?.Priority != null ? Enum.Parse<Priority>(model?.Priority, true) : this.Priority;
            this.Status = model?.Status != null ? Enum.Parse<Status>(model?.Status, true) : this.Status;
        }
        public MyTask(TaskModel model, Guid taskId)
        {
            this.Id = taskId;
            this.Name = model?.Name ?? this.Name;
            this.Description = model?.Description ?? this.Description;
            this.DueDate = model?.DueDate ?? this.DueDate;
            this.StartDate = model?.StartDate ?? this.StartDate;
            this.EndDate = model?.EndDate ?? this.EndDate;
            this.Priority = model?.Priority != null ? Enum.Parse<Priority>(model?.Priority, true) : this.Priority;
            this.Status = model?.Status != null ? Enum.Parse<Status>(model?.Status, true) : this.Status;
        }

        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Priority? Priority { get; set; }
        public Status? Status { get; set; }

    }
}
