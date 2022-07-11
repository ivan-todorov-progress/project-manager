import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskType, TaskTypeMap, TaskStatus, TaskStatusMap, TaskGroup } from '../app.model';
import { NavMenuItem, menuItems } from '../nav-menu/nav-menu.component';
import { TaskService } from '../app.services';

@Component({
  selector: 'app-task-board',
  templateUrl: './task-board.component.html',
})
export class TaskBoardComponent implements OnInit, OnDestroy {
  public projectId?: string;
  public taskGroups?: TaskGroup[];

  constructor(private activatedRoute: ActivatedRoute,
              private taskService: TaskService) {
    const projectId = this.activatedRoute.snapshot.paramMap.get('projectId');

    if (projectId != null) {
      this.projectId = projectId;
    }
  }

  ngOnInit(): void {
    if (this.projectId) {
      const menuItem = new NavMenuItem(`/task-list/${this.projectId}`, 'Tasks');

      menuItems.push(menuItem);

      this.taskService.getTasks(this.projectId)
          .subscribe(result => {
            const taskStatuses = TaskStatusMap.statuses();

            this.taskGroups = taskStatuses.map(taskStatus => {
              const taskGroup = new TaskGroup();
              const taskInfos = result.filter(taskInfo =>
                taskInfo.status == taskStatus);

              taskGroup.status = taskStatus;
              taskGroup.tasks = taskInfos;

              return taskGroup;
            });
          },
          error => console.error(error));
    }
  }

  ngOnDestroy(): void {
    if (this.projectId) {
      menuItems.pop();
    }
  }

  getTypeName(taskType?: TaskType): string {
    return TaskTypeMap.get(taskType);
  }

  getStatusName(taskStatus?: TaskStatus): string {
    return TaskStatusMap.get(taskStatus);
  }
}
