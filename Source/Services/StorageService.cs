using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public class StorageService : IStorageService
{
    private const string projectsName = "Projects.json";
    private const string tasksName = "Tasks.json";
    private const string changesName = "Changes.json";

    private readonly IStorageProvider storageProvider;
    private readonly JsonSerializerOptions jsonOptions;

    public StorageService(IStorageProvider storageProvider)
    {
        this.storageProvider = storageProvider;
        this.jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
    }

    public async Task<IEnumerable<ProjectInfo>> LoadProjectsAsync()
    {
        if (this.storageProvider.Exists(projectsName))
        {
            using var fileStream = this.storageProvider.Open(projectsName);
            var projectInfos = await this.DeserializeAsync<IEnumerable<ProjectInfo>>(fileStream);

            if (projectInfos is not null)
            {
                return projectInfos;
            }
        }

        return Enumerable.Empty<ProjectInfo>();
    }

    public async Task SaveProjectsAsync(IEnumerable<ProjectInfo> projectInfos)
    {
        using var fileStream = this.storageProvider.Create(projectsName);

        await this.SerializeAsync(fileStream, projectInfos);
    }

    public async Task<IEnumerable<TaskInfo>> LoadTasksAsync(string projectId)
    {
        var filePath = Path.Combine(projectId, tasksName);

        if (this.storageProvider.Exists(filePath))
        {
            using var fileStream = this.storageProvider.Open(filePath);
            var taskInfos = await this.DeserializeAsync<IEnumerable<TaskInfo>>(fileStream);

            if (taskInfos is not null)
            {
                return taskInfos;
            }
        }

        return Enumerable.Empty<TaskInfo>();
    }

    public async Task SaveTasksAsync(string projectId, IEnumerable<TaskInfo> taskInfos)
    {
        var filePath = Path.Combine(projectId, tasksName);
        using var fileStream = this.storageProvider.Create(filePath);

        await this.SerializeAsync(fileStream, taskInfos);
    }

    public async Task<IEnumerable<ChangeSet>> LoadChangesAsync(string parentId)
    {
        var filePath = Path.Combine(parentId, changesName);

        if (this.storageProvider.Exists(filePath))
        {
            using var fileStream = this.storageProvider.Open(filePath);
            var changeSets = await this.DeserializeAsync<IEnumerable<ChangeSet>>(fileStream);

            if (changeSets is not null)
            {
                return changeSets;
            }
        }

        return Enumerable.Empty<ChangeSet>();
    }

    public async Task SaveChangesAsync(string parentId, IEnumerable<ChangeSet> changeSets)
    {
        var filePath = Path.Combine(parentId, changesName);
        using var fileStream = this.storageProvider.Create(filePath);

        await this.SerializeAsync(fileStream, changeSets);
    }

    private async Task<TDataObject?> DeserializeAsync<TDataObject>(Stream jsonStream)
    {
        return await JsonSerializer.DeserializeAsync<TDataObject>(jsonStream, this.jsonOptions);
    }

    private async Task SerializeAsync<TDataObject>(Stream jsonStream, TDataObject dataObject)
    {
        await JsonSerializer.SerializeAsync(jsonStream, dataObject, this.jsonOptions);
    }
}
