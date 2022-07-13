using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Telerik.Project.Management.Models;

using TaskStatus = Telerik.Project.Management.Models.TaskStatus;

namespace Telerik.Project.Management.Services.Tests;

[TestClass]
public class TaskServiceTests
{
    [TestMethod]
    public async Task CreateTask_ShouldAddTask()
    {
        var storageService = new Mock<IStorageService>();
        var taskService = new TaskService(storageService.Object);
        var createdInfos = Enumerable.Empty<TaskInfo>();
        var projectId = UniqueKey.New();

        storageService.Setup(mock => mock.LoadTasksAsync(projectId))
                      .Returns(Task.FromResult(Enumerable.Empty<TaskInfo>()));
        storageService.Setup(mock => mock.SaveTasksAsync(projectId, It.IsAny<IEnumerable<TaskInfo>>()))
                      .Callback<string, IEnumerable<TaskInfo>>((id, items) => createdInfos = items)
                      .Returns(Task.CompletedTask);

        var taskInfo = new TaskInfo
        {
            Type = TaskType.Bug,
            Status = TaskStatus.InProgress,
            Priority = TaskPriority.Normal,
            Title = "Title",
            Description = "Description",
            Assignee = "Assignee",
            Estimate = 2
        };

        await taskService.CreateTaskAsync(projectId, taskInfo);

        var createdInfo = createdInfos.FirstOrDefault();

        Assert.IsNotNull(createdInfo);
        Assert.IsNotNull(createdInfo.Id);
        Assert.AreEqual(TaskType.Bug, createdInfo.Type);
        Assert.AreEqual(TaskStatus.InProgress, createdInfo.Status);
        Assert.AreEqual(TaskPriority.Normal, createdInfo.Priority);
        Assert.AreEqual("Title", createdInfo.Title);
        Assert.AreEqual("Description", createdInfo.Description);
        Assert.AreEqual("Assignee", createdInfo.Assignee);
        Assert.AreEqual(2, createdInfo.Estimate);
        Assert.IsNotNull(createdInfo.Created);
        Assert.IsNotNull(createdInfo.Modified);
    }

    [TestMethod]
    public async Task CreateTask_ShouldAddHistory()
    {
        var storageService = new Mock<IStorageService>();
        var taskService = new TaskService(storageService.Object);
        var changeSets = Enumerable.Empty<ChangeSet>();
        var projectId = UniqueKey.New();

        storageService.Setup(mock => mock.LoadTasksAsync(projectId))
                      .Returns(Task.FromResult(Enumerable.Empty<TaskInfo>()));
        storageService.Setup(mock => mock.SaveChangesAsync(It.IsAny<string>(), It.IsAny<IEnumerable<ChangeSet>>()))
                      .Callback<string, IEnumerable<ChangeSet>>((id, items) => changeSets = items)
                      .Returns(Task.CompletedTask);

        var taskInfo = new TaskInfo
        {
            Type = TaskType.Bug,
            Status = TaskStatus.InProgress,
            Priority = TaskPriority.Normal,
            Title = "Title",
            Description = "Description",
            Assignee = "Assignee",
            Estimate = 2
        };

        await taskService.CreateTaskAsync(projectId, taskInfo);

        var changeSet = changeSets.FirstOrDefault();

        Assert.IsNotNull(changeSet);

        var typeChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Type));

        Assert.IsNotNull(typeChangeInfo);
        Assert.IsNull(typeChangeInfo.OldValue);
        Assert.AreEqual(TaskType.Bug, typeChangeInfo.NewValue);

        var statusChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Status));

        Assert.IsNotNull(statusChangeInfo);
        Assert.IsNull(statusChangeInfo.OldValue);
        Assert.AreEqual(TaskStatus.InProgress, statusChangeInfo.NewValue);

        var priorityChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Priority));

        Assert.IsNotNull(priorityChangeInfo);
        Assert.IsNull(priorityChangeInfo.OldValue);
        Assert.AreEqual(TaskPriority.Normal, priorityChangeInfo.NewValue);

        var titleChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Title));

        Assert.IsNotNull(titleChangeInfo);
        Assert.IsNull(titleChangeInfo.OldValue);
        Assert.AreEqual("Title", titleChangeInfo.NewValue);

        var descriptionChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Description));

        Assert.IsNotNull(descriptionChangeInfo);
        Assert.IsNull(descriptionChangeInfo.OldValue);
        Assert.AreEqual("Description", descriptionChangeInfo.NewValue);

        var assigneeChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Assignee));

        Assert.IsNotNull(assigneeChangeInfo);
        Assert.IsNull(assigneeChangeInfo.OldValue);
        Assert.AreEqual("Assignee", assigneeChangeInfo.NewValue);

        var estimateChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Estimate));

        Assert.IsNotNull(estimateChangeInfo);
        Assert.IsNull(estimateChangeInfo.OldValue);
        Assert.AreEqual(2d, estimateChangeInfo.NewValue);
    }

    [TestMethod]
    public async Task UpdateTask_ShouldUpdateTask()
    {
        var storageService = new Mock<IStorageService>();
        var taskService = new TaskService(storageService.Object);
        var updatedInfos = Enumerable.Empty<TaskInfo>();
        var projectId = UniqueKey.New();
        var taskId = UniqueKey.New();
        var originalInfos = new[]
        {
            new TaskInfo
            {
                Id = taskId,
                Type = TaskType.Bug,
                Status = TaskStatus.InProgress,
                Priority = TaskPriority.Normal,
                Title = "Old Title",
                Description = "Old Description",
                Assignee = "Old Assignee",
                Estimate = 2
            }
        };

        storageService.Setup(mock => mock.LoadTasksAsync(projectId))
                      .Returns(Task.FromResult(originalInfos.AsEnumerable()));
        storageService.Setup(mock => mock.SaveTasksAsync(projectId, It.IsAny<IEnumerable<TaskInfo>>()))
                      .Callback<string, IEnumerable<TaskInfo>>((id, items) => updatedInfos = items)
                      .Returns(Task.CompletedTask);

        var taskInfo = new TaskInfo
        {
            Id = taskId,
            Type = TaskType.Story,
            Status = TaskStatus.ReadyForTest,
            Priority = TaskPriority.High,
            Title = "New Title",
            Description = "New Description",
            Assignee = "New Assignee",
            Estimate = 5
        };

        await taskService.UpdateTaskAsync(projectId, taskInfo);

        var updatedInfo = updatedInfos.FirstOrDefault();

        Assert.IsNotNull(updatedInfo);
        Assert.AreEqual(taskId, updatedInfo.Id);
        Assert.AreEqual(TaskType.Story, updatedInfo.Type);
        Assert.AreEqual(TaskStatus.ReadyForTest, updatedInfo.Status);
        Assert.AreEqual(TaskPriority.High, updatedInfo.Priority);
        Assert.AreEqual("New Title", updatedInfo.Title);
        Assert.AreEqual("New Description", updatedInfo.Description);
        Assert.AreEqual("New Assignee", updatedInfo.Assignee);
        Assert.AreEqual(5d, updatedInfo.Estimate);
        Assert.IsNotNull(updatedInfo.Modified);
    }

    [TestMethod]
    public async Task UpdateTask_ShouldAddHistory()
    {
        var storageService = new Mock<IStorageService>();
        var taskService = new TaskService(storageService.Object);
        var changeSets = Enumerable.Empty<ChangeSet>();
        var projectId = UniqueKey.New();
        var taskId = UniqueKey.New();
        var originalInfos = new[]
        {
            new TaskInfo
            {
                Id = taskId,
                Type = TaskType.Bug,
                Status = TaskStatus.InProgress,
                Priority = TaskPriority.Normal,
                Title = "Old Title",
                Description = "Old Description",
                Assignee = "Old Assignee",
                Estimate = 2
            }
        };

        storageService.Setup(mock => mock.LoadTasksAsync(projectId))
                      .Returns(Task.FromResult(originalInfos.AsEnumerable()));
        storageService.Setup(mock => mock.SaveChangesAsync(It.IsAny<string>(), It.IsAny<IEnumerable<ChangeSet>>()))
                      .Callback<string, IEnumerable<ChangeSet>>((id, items) => changeSets = items)
                      .Returns(Task.CompletedTask);

        var taskInfo = new TaskInfo
        {
            Id = taskId,
            Type = TaskType.Story,
            Status = TaskStatus.ReadyForTest,
            Priority = TaskPriority.High,
            Title = "New Title",
            Description = "New Description",
            Assignee = "New Assignee",
            Estimate = 5
        };

        await taskService.UpdateTaskAsync(projectId, taskInfo);

        var changeSet = changeSets.FirstOrDefault();

        Assert.IsNotNull(changeSet);

        var typeChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Type));

        Assert.IsNotNull(typeChangeInfo);
        Assert.AreEqual(TaskType.Bug, typeChangeInfo.OldValue);
        Assert.AreEqual(TaskType.Story, typeChangeInfo.NewValue);

        var statusChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Status));

        Assert.IsNotNull(statusChangeInfo);
        Assert.AreEqual(TaskStatus.InProgress, statusChangeInfo.OldValue);
        Assert.AreEqual(TaskStatus.ReadyForTest, statusChangeInfo.NewValue);

        var priorityChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Priority));

        Assert.IsNotNull(priorityChangeInfo);
        Assert.AreEqual(TaskPriority.Normal, priorityChangeInfo.OldValue);
        Assert.AreEqual(TaskPriority.High, priorityChangeInfo.NewValue);

        var titleChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Title));

        Assert.IsNotNull(titleChangeInfo);
        Assert.AreEqual("Old Title", titleChangeInfo.OldValue);
        Assert.AreEqual("New Title", titleChangeInfo.NewValue);

        var descriptionChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Description));

        Assert.IsNotNull(descriptionChangeInfo);
        Assert.AreEqual("Old Description", descriptionChangeInfo.OldValue);
        Assert.AreEqual("New Description", descriptionChangeInfo.NewValue);

        var assigneeChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Assignee));

        Assert.IsNotNull(assigneeChangeInfo);
        Assert.AreEqual("Old Assignee", assigneeChangeInfo.OldValue);
        Assert.AreEqual("New Assignee", assigneeChangeInfo.NewValue);

        var estimateChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(taskInfo.Estimate));

        Assert.IsNotNull(estimateChangeInfo);
        Assert.AreEqual(2d, estimateChangeInfo.OldValue);
        Assert.AreEqual(5d, estimateChangeInfo.NewValue);
    }
}
