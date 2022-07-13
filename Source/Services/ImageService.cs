using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public class ImageService : IImageService
{
    private readonly IStorageProvider storageProvider;

    public ImageService(IStorageProvider storageProvider)
    {
        this.storageProvider = storageProvider;
    }

    public async Task<ImageInfo> SaveImageAsync(string projectId, IFormFile imageFile)
    {
        var imageName = imageFile.FileName;
        var imagePath = Path.Combine(projectId, imageName);
        var imageUrl = this.storageProvider.Url(imagePath);
        using var imageStream = this.storageProvider.Create(imagePath);

        await imageFile.CopyToAsync(imageStream);

        return new ImageInfo
        {
            Name = imageName,
            Url = imageUrl
        };
    }
}
