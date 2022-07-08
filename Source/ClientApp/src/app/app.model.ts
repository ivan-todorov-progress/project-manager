
export class ProjectInfo {
  id?: string;
  title?: string;
  image?: string;
  description?: string;
  created?: Date;
  modified?: Date;
}

export enum TaskType {
  story = 'story',
  bug = 'bug',
}

export class TaskTypeEntry {
  constructor(public type: TaskType, public name: string) {
  }
}

export class TaskTypeMap {
  private static mapping = [
    new TaskTypeEntry(TaskType.story, 'Story'),
    new TaskTypeEntry(TaskType.bug, 'Bug')
  ];

  static entries(): TaskTypeEntry[] {
    return this.mapping;
  }

  static types(): TaskType[] {
    return this.mapping.map(entry => entry.type);
  }

  static names(): string[] {
    return this.mapping.map(entry => entry.name);
  }

  static get(type?: TaskType): string {
    const entry = this.mapping.find(entry => entry.type == type);

    if (entry) {
      return entry.name;
    }

    return '(none)';
  }
}

export enum TaskStatus {
  toDo = 'toDo',
  inProgress = 'inProgress',
  readyForTest = 'readyForTest',
  done = 'done'
}

export class TaskStatusEntry {
  constructor(public status: TaskStatus, public name: string) {
  }
}

export class TaskStatusMap {
  private static mapping = [
    new TaskStatusEntry(TaskStatus.toDo, 'To Do'),
    new TaskStatusEntry(TaskStatus.inProgress, 'In Progress'),
    new TaskStatusEntry(TaskStatus.readyForTest, 'Ready for Test'),
    new TaskStatusEntry(TaskStatus.done, 'Done')
  ];

  static entries(): TaskStatusEntry[] {
    return this.mapping;
  }

  static statuses(): TaskStatus[] {
    return this.mapping.map(entry => entry.status);
  }

  static names(): string[] {
    return this.mapping.map(entry => entry.name);
  }

  static get(status?: TaskStatus): string {
    const entry = this.mapping.find(entry => entry.status == status);

    if (entry) {
      return entry.name;
    }

    return '(none)';
  }
}

export enum TaskPriority {
  low = 'low',
  normal = 'normal',
  high = 'high',
  critical = 'critical'
}

export class TaskPriorityEntry {
  constructor(public priority: TaskPriority, public name: string) {
  }
}

export class TaskPriorityMap {
  private static mapping = [
    new TaskPriorityEntry(TaskPriority.low, 'Low'),
    new TaskPriorityEntry(TaskPriority.normal, 'Normal'),
    new TaskPriorityEntry(TaskPriority.high, 'High'),
    new TaskPriorityEntry(TaskPriority.critical, 'Critical')
  ];

  static entries(): TaskPriorityEntry[] {
    return this.mapping;
  }

  static priorities(): TaskPriority[] {
    return this.mapping.map(entry => entry.priority);
  }

  static names(): string[] {
    return this.mapping.map(entry => entry.name);
  }

  static get(priority?: TaskPriority): string {
    const entry = this.mapping.find(entry => entry.priority == priority);

    if (entry) {
      return entry.name;
    }

    return '(none)';
  }
}

export class TaskInfo {
  id?: string;
  type?: TaskType;
  status?: TaskStatus;
  priority?: TaskPriority;
  title?: string;
  description?: string;
  assignee?: string;
  estimate?: number;
  created?: Date;
  modified?: Date;
}

export class TaskGroup {
  status?: TaskStatus;
  tasks?: TaskInfo[];
}

export class ChangeSet {
  id?: string;
  modified?: Date;
  changes?: ChangeInfo[];
}

export class ChangeInfo {
  name?: string;
  oldValue?: object;
  newValue?: object;
}

export class ImageInfo {
  name?: string;
  url?: string;
}
