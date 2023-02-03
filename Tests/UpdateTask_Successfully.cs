using Moq;
using RESTful_Web_Service.Entities;
using RESTful_Web_Service.Enums;
using RESTful_Web_Service.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class UpdateTask_Successfully
    {
        private readonly ITaskService taskService;
        public UpdateTask_Successfully()
        {
            var commandMock = new Mock<IDbCommand>();
            commandMock.Setup(m => m.ExecuteScalar()).Returns(0).Verifiable();
            commandMock.Setup(x => x.Parameters.Clear()).Verifiable();
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(m => m.CreateCommand()).Returns(commandMock.Object);
            this.taskService = new TaskService(connectionMock.Object);
        }

        [Fact]
        public async Task Execute()
        {
            var task = new MyTask()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Description test",
                DueDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Priority = Priority.high,
                Status = Status.inProgress
            };
            var result = await taskService.CreateTask(task);
            Assert.True(result);
        }
    }
}
