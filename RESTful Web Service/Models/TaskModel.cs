using RESTful_Web_Service.Entities;
using RESTful_Web_Service.Enums;
using System.ComponentModel.DataAnnotations;

namespace RESTful_Web_Service.Models
{
    public class TaskModel
    {
       
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
    }
}
