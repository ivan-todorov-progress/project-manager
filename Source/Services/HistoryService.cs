using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public class HistoryService : IHistoryService
{
    private readonly IStorageService storageService;

    public HistoryService(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<IEnumerable<ChangeSet>> GetChangesAsync(string parentId)
    {
        return await this.storageService.LoadChangesAsync(parentId);
    }
}
