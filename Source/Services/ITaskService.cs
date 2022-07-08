using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskInfo>> GetTasksAsync(string projectId);
    Task<TaskInfo> GetTaskAsync(string projectId, string taskId);
    Task<TaskInfo> CreateTaskAsync(string projectId, TaskInfo taskInfo);
    Task<TaskInfo> UpdateTaskAsync(string projectId, TaskInfo taskInfo);
}