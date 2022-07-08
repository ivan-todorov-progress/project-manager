using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public class TaskService : ITaskService
{
    private readonly IStorageService storageService;

    public TaskService(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<IEnumerable<TaskInfo>> GetTasksAsync(string projectId)
    {
        return await this.storageService.LoadTasksAsync(projectId);
    }

    public async Task<TaskInfo> GetTaskAsync(string projectId, string taskId)
    {
        var taskInfos = await this.storageService.LoadTasksAsync(projectId);
        var taskInfo = taskInfos.FirstOrDefault(currentInfo => currentInfo.Id == taskId);

        if (taskInfo is null)
        {
            var errorMessage = $"Task not found: {taskId}.";

            throw new ArgumentException(errorMessage);
        }

        return taskInfo;
    }

    public async Task<TaskInfo> CreateTaskAsync(string projectId, TaskInfo taskInfo)
    {
        var taskId = UniqueKey.New();
        var currentTime = DateTime.Now;

        taskInfo.Id = taskId;
        taskInfo.Created = currentTime;
        taskInfo.Modified = currentTime;

        var taskInfos = await this.storageService.LoadTasksAsync(projectId);
        var taskCollection = new List<TaskInfo>(taskInfos);

        taskCollection.Add(taskInfo);

        await this.storageService.SaveTasksAsync(projectId, taskCollection);

        var changeId = UniqueKey.New();
        var changeInfos = DiffChanges.Get(taskInfo);
        var changeSet = new ChangeSet();

        changeSet.Id = changeId;
        changeSet.Modified = currentTime;
        changeSet.Changes = changeInfos;

        var changeSets = await this.storageService.LoadChangesAsync(taskId);
        var changeCollection = new List<ChangeSet>(changeSets);

        changeCollection.Add(changeSet);

        await this.storageService.SaveChangesAsync(taskId, changeCollection);

        return taskInfo;
    }

    public async Task<TaskInfo> UpdateTaskAsync(string projectId, TaskInfo taskInfo)
    {
        var taskId = taskInfo.Id;

        if (taskId is null)
        {
            throw new ArgumentNullException(nameof(taskId));
        }

        var currentTime = DateTime.Now;

        taskInfo.Modified = currentTime;

        var taskInfos = await this.storageService.LoadTasksAsync(projectId);
        var taskCollection = new List<TaskInfo>(taskInfos);
        var taskIndex = taskCollection.FindIndex(currentInfo => currentInfo.Id == taskId);

        if (taskIndex < 0)
        {
            var errorMessage = $"Task not found: {taskId}.";

            throw new ArgumentException(errorMessage);
        }

        var originalInfo = taskCollection[taskIndex];
        var changeInfos = DiffChanges.Get(originalInfo, taskInfo);

        taskCollection[taskIndex] = taskInfo;

        await this.storageService.SaveTasksAsync(projectId, taskCollection);

        var changeId = UniqueKey.New();
        var changeSet = new ChangeSet();

        changeSet.Id = changeId;
        changeSet.Modified = currentTime;
        changeSet.Changes = changeInfos;

        var changeSets = await this.storageService.LoadChangesAsync(taskId);
        var changeCollection = new List<ChangeSet>(changeSets);

        changeCollection.Add(changeSet);

        await this.storageService.SaveChangesAsync(taskId, changeCollection);

        return taskInfo;
    }
}
