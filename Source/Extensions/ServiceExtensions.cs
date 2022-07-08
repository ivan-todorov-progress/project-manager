using Telerik.Project.Management.Services;

namespace Telerik.Project.Management;

public static class ServiceExtensions
{
    public static void AddDataServices(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddSingleton<IStorageProvider, StorageProvider>();
        serviceDescriptors.AddSingleton<IStorageService, StorageService>();
        serviceDescriptors.AddSingleton<IProjectService, ProjectService>();
        serviceDescriptors.AddSingleton<ITaskService, TaskService>();
        serviceDescriptors.AddSingleton<IHistoryService, HistoryService>();
        serviceDescriptors.AddSingleton<IImageService, ImageService>();
    }
}
