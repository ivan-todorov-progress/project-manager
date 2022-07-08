using Telerik.Project.Management.Models;

namespace Telerik.Project.Management.Services;

public interface IHistoryService
{
    Task<IEnumerable<ChangeSet>> GetChangesAsync(string parentId);
}
