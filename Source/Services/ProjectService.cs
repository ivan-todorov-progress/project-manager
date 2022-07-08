using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public class ProjectService : IProjectService
{
    private readonly IStorageService storageService;

    public ProjectService(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<IEnumerable<ProjectInfo>> GetProjectsAsync()
    {
        return await this.storageService.LoadProjectsAsync();
    }

    public async Task<ProjectInfo> GetProjectAsync(string projectId)
    {
        var projectInfos = await this.storageService.LoadProjectsAsync();
        var projectInfo = projectInfos.FirstOrDefault(currentInfo => currentInfo.Id == projectId);

        if (projectInfo is null)
        {
            var errorMessage = $"Project not found: {projectId}.";

            throw new ArgumentException(errorMessage);
        }

        return projectInfo;
    }

    public async Task<ProjectInfo> CreateProjectAsync(ProjectInfo projectInfo)
    {
        var projectId = UniqueKey.New();
        var currentTime = DateTime.Now;

        projectInfo.Id = projectId;
        projectInfo.Created = currentTime;
        projectInfo.Modified = currentTime;

        var projectInfos = await this.storageService.LoadProjectsAsync();
        var projectCollection = new List<ProjectInfo>(projectInfos);

        projectCollection.Add(projectInfo);

        await this.storageService.SaveProjectsAsync(projectCollection);

        var changeId = UniqueKey.New();
        var changeInfos = DiffChanges.Get(projectInfo);
        var changeSet = new ChangeSet();

        changeSet.Id = changeId;
        changeSet.Modified = currentTime;
        changeSet.Changes = changeInfos;

        var changeSets = await this.storageService.LoadChangesAsync(projectId);
        var changeCollection = new List<ChangeSet>(changeSets);

        changeCollection.Add(changeSet);

        await this.storageService.SaveChangesAsync(projectId, changeCollection);

        return projectInfo;
    }

    public async Task<ProjectInfo> UpdateProjectAsync(ProjectInfo projectInfo)
    {
        var projectId = projectInfo.Id;

        if (projectId is null)
        {
            throw new ArgumentNullException(nameof(projectId));
        }

        var currentTime = DateTime.Now;

        projectInfo.Modified = currentTime;

        var projectInfos = await this.storageService.LoadProjectsAsync();
        var projectCollection = new List<ProjectInfo>(projectInfos);
        var projectIndex = projectCollection.FindIndex(currentInfo => currentInfo.Id == projectId);

        if (projectIndex < 0)
        {
            var errorMessage = $"Project not found: {projectId}.";

            throw new ArgumentException(errorMessage);
        }

        var originalInfo = projectCollection[projectIndex];
        var changeInfos = DiffChanges.Get(originalInfo, projectInfo);

        projectCollection[projectIndex] = projectInfo;

        await this.storageService.SaveProjectsAsync(projectCollection);

        var changeId = UniqueKey.New();
        var changeSet = new ChangeSet();

        changeSet.Id = changeId;
        changeSet.Modified = currentTime;
        changeSet.Changes = changeInfos;

        var changeSets = await this.storageService.LoadChangesAsync(projectId);
        var changeCollection = new List<ChangeSet>(changeSets);

        changeCollection.Add(changeSet);

        await this.storageService.SaveChangesAsync(projectId, changeCollection);

        return projectInfo;
    }
}
