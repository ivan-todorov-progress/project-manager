using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services.Tests;

[TestClass]
public class ProjectServiceTests
{
    [TestMethod]
    public async Task CreateProject_ShouldAddProject()
    {
        var storageService = new Mock<IStorageService>();
        var projectService = new ProjectService(storageService.Object);
        var createdInfos = Enumerable.Empty<ProjectInfo>();

        storageService.Setup(mock => mock.LoadProjectsAsync())
                      .Returns(Task.FromResult(Enumerable.Empty<ProjectInfo>()));
        storageService.Setup(mock => mock.SaveProjectsAsync(It.IsAny<IEnumerable<ProjectInfo>>()))
                      .Callback<IEnumerable<ProjectInfo>>(items => createdInfos = items)
                      .Returns(Task.CompletedTask);

        var projectInfo = new ProjectInfo
        {
            Title = "Title",
            Image = "Image",
            Description = "Description"
        };

        await projectService.CreateProjectAsync(projectInfo);

        var createdInfo = createdInfos.FirstOrDefault();

        Assert.IsNotNull(createdInfo);
        Assert.IsNotNull(createdInfo.Id);
        Assert.AreEqual("Title", createdInfo.Title);
        Assert.AreEqual("Image", createdInfo.Image);
        Assert.AreEqual("Description", createdInfo.Description);
        Assert.IsNotNull(createdInfo.Created);
        Assert.IsNotNull(createdInfo.Modified);
    }

    [TestMethod]
    public async Task CreateProject_ShouldAddHistory()
    {
        var storageService = new Mock<IStorageService>();
        var projectService = new ProjectService(storageService.Object);
        var changeSets = Enumerable.Empty<ChangeSet>();

        storageService.Setup(mock => mock.LoadProjectsAsync())
                      .Returns(Task.FromResult(Enumerable.Empty<ProjectInfo>()));
        storageService.Setup(mock => mock.SaveChangesAsync(It.IsAny<string>(), It.IsAny<IEnumerable<ChangeSet>>()))
                      .Callback<string, IEnumerable<ChangeSet>>((id, items) => changeSets = items)
                      .Returns(Task.CompletedTask);

        var projectInfo = new ProjectInfo
        {
            Title = "Title",
            Image = "Image",
            Description = "Description"
        };

        await projectService.CreateProjectAsync(projectInfo);

        var changeSet = changeSets.FirstOrDefault();

        Assert.IsNotNull(changeSet);

        var titleChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(projectInfo.Title));

        Assert.IsNotNull(titleChangeInfo);
        Assert.IsNull(titleChangeInfo.OldValue);
        Assert.AreEqual("Title", titleChangeInfo.NewValue);

        var imageChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(projectInfo.Image));

        Assert.IsNotNull(imageChangeInfo);
        Assert.IsNull(imageChangeInfo.OldValue);
        Assert.AreEqual("Image", imageChangeInfo.NewValue);

        var descriptionChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(projectInfo.Description));

        Assert.IsNotNull(descriptionChangeInfo);
        Assert.IsNull(descriptionChangeInfo.OldValue);
        Assert.AreEqual("Description", descriptionChangeInfo.NewValue);
    }

    [TestMethod]
    public async Task UpdateProject_ShouldUpdateProject()
    {
        var storageService = new Mock<IStorageService>();
        var projectService = new ProjectService(storageService.Object);
        var updatedInfos = Enumerable.Empty<ProjectInfo>();
        var projectId = UniqueKey.New();
        var originalInfos = new[]
        {
            new ProjectInfo
            {
                Id = projectId,
                Title = "Old Title",
                Image = "Old Image",
                Description = "Old Description"
            }
        };

        storageService.Setup(mock => mock.LoadProjectsAsync())
                      .Returns(Task.FromResult(originalInfos.AsEnumerable()));
        storageService.Setup(mock => mock.SaveProjectsAsync(It.IsAny<IEnumerable<ProjectInfo>>()))
                      .Callback<IEnumerable<ProjectInfo>>(items => updatedInfos = items)
                      .Returns(Task.CompletedTask);

        var projectInfo = new ProjectInfo
        {
            Id = projectId,
            Title = "New Title",
            Image = "New Image",
            Description = "New Description"
        };

        await projectService.UpdateProjectAsync(projectInfo);

        var updatedInfo = updatedInfos.FirstOrDefault();

        Assert.IsNotNull(updatedInfo);
        Assert.AreEqual(projectId, updatedInfo.Id);
        Assert.AreEqual("New Title", updatedInfo.Title);
        Assert.AreEqual("New Image", updatedInfo.Image);
        Assert.AreEqual("New Description", updatedInfo.Description);
        Assert.IsNotNull(updatedInfo.Modified);
    }

    [TestMethod]
    public async Task UpdateProject_ShouldAddHistory()
    {
        var storageService = new Mock<IStorageService>();
        var projectService = new ProjectService(storageService.Object);
        var changeSets = Enumerable.Empty<ChangeSet>();
        var projectId = UniqueKey.New();
        var originalInfos = new[]
        {
            new ProjectInfo
            {
                Id = projectId,
                Title = "Old Title",
                Image = "Old Image",
                Description = "Old Description"
            }
        };

        storageService.Setup(mock => mock.LoadProjectsAsync())
                      .Returns(Task.FromResult(originalInfos.AsEnumerable()));
        storageService.Setup(mock => mock.SaveChangesAsync(It.IsAny<string>(), It.IsAny<IEnumerable<ChangeSet>>()))
                      .Callback<string, IEnumerable<ChangeSet>>((id, items) => changeSets = items)
                      .Returns(Task.CompletedTask);

        var projectInfo = new ProjectInfo
        {
            Id = projectId,
            Title = "New Title",
            Image = "New Image",
            Description = "New Description"
        };

        await projectService.UpdateProjectAsync(projectInfo);

        var changeSet = changeSets.FirstOrDefault();

        Assert.IsNotNull(changeSet);

        var titleChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(projectInfo.Title));

        Assert.IsNotNull(titleChangeInfo);
        Assert.AreEqual("Old Title", titleChangeInfo.OldValue);
        Assert.AreEqual("New Title", titleChangeInfo.NewValue);

        var imageChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(projectInfo.Image));

        Assert.IsNotNull(imageChangeInfo);
        Assert.AreEqual("Old Image", imageChangeInfo.OldValue);
        Assert.AreEqual("New Image", imageChangeInfo.NewValue);

        var descriptionChangeInfo = changeSet.Changes
            .FirstOrDefault(item => item.Name == nameof(projectInfo.Description));

        Assert.IsNotNull(descriptionChangeInfo);
        Assert.AreEqual("Old Description", descriptionChangeInfo.OldValue);
        Assert.AreEqual("New Description", descriptionChangeInfo.NewValue);
    }
}
