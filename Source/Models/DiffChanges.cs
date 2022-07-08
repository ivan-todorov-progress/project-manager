namespace Telerik.Project.Management.Models;

public static class DiffChanges
{
    public static IEnumerable<ChangeInfo> Get(ProjectInfo projectInfo)
    {
        var changeInfos = new List<ChangeInfo>();

        if (projectInfo.Title is not null)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(projectInfo.Title),
                NewValue = projectInfo.Title
            };

            changeInfos.Add(changeInfo);
        }

        if (projectInfo.Image is not null)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(projectInfo.Image),
                NewValue = projectInfo.Image
            };

            changeInfos.Add(changeInfo);
        }

        if (projectInfo.Description is not null)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(projectInfo.Description),
                NewValue = projectInfo.Description
            };

            changeInfos.Add(changeInfo);
        }

        return changeInfos;
    }

    public static IEnumerable<ChangeInfo> Get(ProjectInfo oldInfo, ProjectInfo newInfo)
    {
        var changeInfos = new List<ChangeInfo>();

        if (oldInfo.Title != newInfo.Title)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Title),
                OldValue = oldInfo.Title,
                NewValue = newInfo.Title
            };

            changeInfos.Add(changeInfo);
        }

        if (oldInfo.Image != newInfo.Image)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Image),
                OldValue = oldInfo.Image,
                NewValue = newInfo.Image
            };

            changeInfos.Add(changeInfo);
        }

        if (oldInfo.Description != newInfo.Description)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Description),
                OldValue = oldInfo.Description,
                NewValue = newInfo.Description
            };

            changeInfos.Add(changeInfo);
        }

        return changeInfos;
    }

    public static IEnumerable<ChangeInfo> Get(TaskInfo newInfo)
    {
        var changeInfos = new List<ChangeInfo>
        {
            new ChangeInfo
            {
                Name = nameof(newInfo.Type),
                NewValue = newInfo.Type
            },
            new ChangeInfo
            {
                Name = nameof(newInfo.Status),
                NewValue = newInfo.Status
            },
            new ChangeInfo
            {
                Name = nameof(newInfo.Priority),
                NewValue = newInfo.Priority
            }
        };

        if (newInfo.Title is not null)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Title),
                NewValue = newInfo.Title
            };

            changeInfos.Add(changeInfo);
        }

        if (newInfo.Description is not null)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Description),
                NewValue = newInfo.Description
            };

            changeInfos.Add(changeInfo);
        }

        if (newInfo.Assignee is not null)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Assignee),
                NewValue = newInfo.Assignee
            };

            changeInfos.Add(changeInfo);
        }

        if (newInfo.Estimate is not null)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Estimate),
                NewValue = newInfo.Estimate
            };

            changeInfos.Add(changeInfo);
        }

        return changeInfos;
    }

    public static IEnumerable<ChangeInfo> Get(TaskInfo oldInfo, TaskInfo newInfo)
    {
        var changeInfos = new List<ChangeInfo>();

        if (oldInfo.Type != newInfo.Type)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Type),
                OldValue = oldInfo.Type,
                NewValue = newInfo.Type
            };

            changeInfos.Add(changeInfo);
        }

        if (oldInfo.Status != newInfo.Status)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Status),
                OldValue = oldInfo.Status,
                NewValue = newInfo.Status
            };

            changeInfos.Add(changeInfo);
        }

        if (oldInfo.Priority != newInfo.Priority)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Priority),
                OldValue = oldInfo.Priority,
                NewValue = newInfo.Priority
            };

            changeInfos.Add(changeInfo);
        }

        if (oldInfo.Title != newInfo.Title)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Title),
                OldValue = oldInfo.Title,
                NewValue = newInfo.Title
            };

            changeInfos.Add(changeInfo);
        }

        if (oldInfo.Description != newInfo.Description)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Description),
                OldValue = oldInfo.Description,
                NewValue = newInfo.Description
            };

            changeInfos.Add(changeInfo);
        }

        if (oldInfo.Assignee != newInfo.Assignee)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Assignee),
                OldValue = oldInfo.Assignee,
                NewValue = newInfo.Assignee
            };

            changeInfos.Add(changeInfo);
        }

        if (oldInfo.Estimate != newInfo.Estimate)
        {
            var changeInfo = new ChangeInfo
            {
                Name = nameof(newInfo.Estimate),
                OldValue = oldInfo.Estimate,
                NewValue = newInfo.Estimate
            };

            changeInfos.Add(changeInfo);
        }

        return changeInfos;
    }
}
