using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTful_Web_Service.Entities;
using RESTful_Web_Service.Models;
using RESTful_Web_Service.Services;

namespace RESTful_Web_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel model)
        {
            try
            {
                if (!await taskService.IsModelValid(model))
                {
                    return BadRequest();
                }
                var entity = new MyTask(model);
                var result = await taskService.CreateTask(entity);
                if (!result)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] TaskModel model, [FromQuery] Guid taskId)
        {
            try
            {
                if (!await taskService.IsModelValid(model))
                {
                    return BadRequest();
                }
                var entity = new MyTask(model, taskId);
                var result = await taskService.UpdateTask(entity);
                if (!result)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }
    }
}
