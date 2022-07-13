import { Component, Input } from '@angular/core';
import {
  TaskInfo, TaskType, TaskTypeMap, TaskStatus,
  TaskStatusMap, TaskPriority, TaskPriorityMap
} from '../app.model';

@Component({
  selector: 'app-task-info',
  templateUrl: './task-info.component.html',
})
export class TaskInfoComponent {
  @Input() taskInfo?: TaskInfo;

  getTypeName(taskType?: TaskType): string {
    return TaskTypeMap.get(taskType);
  }

  getStatusName(taskStatus?: TaskStatus): string {
    return TaskStatusMap.get(taskStatus);
  }

  getPriorityName(taskPriority?: TaskPriority): string {
    return TaskPriorityMap.get(taskPriority);
  }
}
