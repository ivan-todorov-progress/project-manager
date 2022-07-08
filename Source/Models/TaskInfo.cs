namespace Telerik.Project.Management.Models;

public class TaskInfo
{
    public string? Id { get; set; }
    public TaskType Type { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Assignee { get; set; }
    public double? Estimate { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Modified { get; set; }
}
