namespace Telerik.Project.Management.Models;

public class ChangeSet
{
    public ChangeSet()
    {
        this.Changes = Enumerable.Empty<ChangeInfo>();
    }

    public string? Id { get; set; }
    public DateTime? Modified { get; set; }
    public IEnumerable<ChangeInfo> Changes { get; set; }
}
