import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskInfo, TaskType, TaskTypeMap, TaskStatus, TaskStatusMap } from '../app.model';
import { TaskService } from '../app.services';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
})
export class TaskListComponent implements OnInit {
  public projectId?: string;
  public taskInfos?: TaskInfo[];

  constructor(private activatedRoute: ActivatedRoute,
              private taskService: TaskService) {
    const projectId = this.activatedRoute.snapshot.paramMap.get('projectId');

    if (projectId != null) {
      this.projectId = projectId;
    }
  }

  ngOnInit(): void {
    if (this.projectId) {
      this.taskService.getTasks(this.projectId)
          .subscribe(result => this.taskInfos = result,
                     error => console.error(error));
    }
  }

  getTypeName(taskType?: TaskType): string {
    return TaskTypeMap.get(taskType);
  }

  getStatusName(taskStatus?: TaskStatus): string {
    return TaskStatusMap.get(taskStatus);
  }
}
