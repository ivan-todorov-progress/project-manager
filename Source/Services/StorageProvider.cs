namespace Telerik.Project.Management.Services;

public class StorageProvider : IStorageProvider
{
    private const string directoryName = "Data";

    private readonly string basePath;

    public StorageProvider(IWebHostEnvironment hostEnvironment)
    {
        var rootPath = hostEnvironment.WebRootPath;

        this.basePath = Path.Combine(rootPath, directoryName);
    }

    public string Url(string filePath)
    {
        var relativePath = Path.Combine(directoryName, filePath);

        return relativePath.Replace('\\', '/');
    }

    public bool Exists(string filePath)
    {
        var absolutePath = Path.Combine(this.basePath, filePath);

        return File.Exists(absolutePath);
    }

    public Stream Create(string filePath)
    {
        var absolutePath = Path.Combine(this.basePath, filePath);
        var directoryPath = Path.GetDirectoryName(absolutePath);

        if (directoryPath is not null && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        return File.Create(absolutePath);
    }

    public Stream Open(string filePath)
    {
        var absolutePath = Path.Combine(this.basePath, filePath);

        return File.OpenRead(absolutePath);
    }

    public void Delete(string filePath)
    {
        var absolutePath = Path.Combine(this.basePath, filePath);

        if (File.Exists(absolutePath))
        {
            File.Delete(absolutePath);
        }
    }
}
