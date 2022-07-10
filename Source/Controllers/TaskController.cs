using Microsoft.AspNetCore.Mvc;
using Telerik.Project.Management.Models;
using Telerik.Project.Management.Services;

namespace Telerik.Project.Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet("{projectId}")]
        public async Task<IEnumerable<TaskInfo>> GetTasksAsync(string projectId)
        {
            return await this.taskService.GetTasksAsync(projectId);
        }

        [HttpGet("{projectId}/{taskId}")]
        public async Task<TaskInfo> GetTaskAsync(string projectId, string taskId)
        {
            return await this.taskService.GetTaskAsync(projectId, taskId);
        }

        [HttpPut("{projectId}")]
        public async Task<TaskInfo> CreateTaskAsync(string projectId, TaskInfo taskInfo)
        {
            return await this.taskService.CreateTaskAsync(projectId, taskInfo);
        }

        [HttpPost("{projectId}")]
        public async Task<TaskInfo> UpdateTaskAsync(string projectId, TaskInfo taskInfo)
        {
            return await this.taskService.UpdateTaskAsync(projectId, taskInfo);
        }
    }
}
