namespace Telerik.Project.Management.Models;

public static class UniqueKey
{
    public static string New()
    {
        var guid = Guid.NewGuid();

        return guid.ToString("N");
    }
}
