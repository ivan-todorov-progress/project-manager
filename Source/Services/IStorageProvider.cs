namespace Telerik.Project.Management.Services;

public interface IStorageProvider
{
    string Url(string filePath);
    bool Exists(string filePath);
    Stream Create(string filePath);
    Stream Open(string filePath);
    void Delete(string filePath);
}