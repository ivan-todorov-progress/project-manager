using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public interface IStorageService
{
    Task<IEnumerable<ProjectInfo>> LoadProjectsAsync();
    Task SaveProjectsAsync(IEnumerable<ProjectInfo> projectInfos);
    Task<IEnumerable<TaskInfo>> LoadTasksAsync(string projectId);
    Task SaveTasksAsync(string projectId, IEnumerable<TaskInfo> taskInfos);
    Task<IEnumerable<ChangeSet>> LoadChangesAsync(string parentId);
    Task SaveChangesAsync(string parentId, IEnumerable<ChangeSet> changeSet);
}