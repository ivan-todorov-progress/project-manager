import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import {
  TaskInfo, TaskType, TaskTypeEntry, TaskTypeMap,
  TaskStatus, TaskStatusEntry, TaskStatusMap,
  TaskPriority, TaskPriorityEntry, TaskPriorityMap
} from '../app.model';
import { TaskService } from '../app.services';

@Component({
  selector: 'app-task-edit',
  templateUrl: './task-edit.component.html',
})
export class TaskEditComponent implements OnInit {
  public projectId?: string;
  public taskId?: string;
  public taskInfo?: TaskInfo;
  public taskTypes: TaskTypeEntry[];
  public taskStatuses: TaskStatusEntry[];
  public taskPriorities: TaskPriorityEntry[];

  constructor(private location: Location,
              private activatedRoute: ActivatedRoute,
              private taskService: TaskService) {
    const projectId = this.activatedRoute.snapshot.paramMap.get('projectId');
    const taskId = this.activatedRoute.snapshot.paramMap.get('taskId');

    if (projectId != null) {
      this.projectId = projectId;
    }

    if (taskId != null) {
      this.taskId = taskId;
    }

    this.taskTypes = TaskTypeMap.entries();
    this.taskStatuses = TaskStatusMap.entries();
    this.taskPriorities = TaskPriorityMap.entries();
  }

  ngOnInit(): void {
    if (this.projectId && this.taskId) {
      this.taskService.getTask(this.projectId, this.taskId)
          .subscribe(result => this.taskInfo = result,
                     error => console.error(error));
    } else {
      this.taskInfo = new TaskInfo();
      this.taskInfo.type = TaskType.story;
      this.taskInfo.status = TaskStatus.toDo;
      this.taskInfo.priority = TaskPriority.normal;
    }
  }

  save(): void {
    if (this.projectId && this.taskInfo) {
      if (this.taskId) {
        this.taskService.updateTask(this.projectId, this.taskInfo)
            .subscribe(result => this.location.back(),
                       error => console.error(error));
      } else {
        this.taskService.createTask(this.projectId, this.taskInfo)
            .subscribe(result => this.location.back(),
                       error => console.error(error));
      }
    }
  }

  cancel(): void {
    this.location.back();
  }
}
