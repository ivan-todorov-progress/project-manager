using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectInfo>> GetProjectsAsync();
    Task<ProjectInfo> GetProjectAsync(string projectId);
    Task<ProjectInfo> CreateProjectAsync(ProjectInfo projectInfo);
    Task<ProjectInfo> UpdateProjectAsync(ProjectInfo projectInfo);
}