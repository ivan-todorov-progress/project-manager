using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public interface IImageService
{
    Task<ImageInfo> SaveImageAsync(string projectId, IFormFile imageFile);
}