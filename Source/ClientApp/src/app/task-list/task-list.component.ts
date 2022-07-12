import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskInfo, TaskType, TaskTypeMap, TaskStatus, TaskStatusMap } from '../app.model';
import { NavMenuItem, menuItems } from '../nav-menu/nav-menu.component';
import { TaskService } from '../app.services';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
})
export class TaskListComponent implements OnInit, OnDestroy {
  public projectId?: string;
  public searchFilter?: string;
  public taskInfos?: TaskInfo[];
  public searchInfos?: TaskInfo[];

  constructor(private activatedRoute: ActivatedRoute,
              private taskService: TaskService) {
    const projectId = this.activatedRoute.snapshot.paramMap.get('projectId');

    if (projectId != null) {
      this.projectId = projectId;
    }
  }

  ngOnInit(): void {
    if (this.projectId) {
      const menuItem = new NavMenuItem(`/task-board/${this.projectId}`, 'Dashboard');

      menuItems.push(menuItem);

      this.taskService.getTasks(this.projectId)
          .subscribe(result => {
            this.taskInfos = result;
            this.searchInfos = result;
          },
          error => console.error(error));
    }
  }

  ngOnDestroy(): void {
    if (this.projectId) {
      menuItems.pop();
    }
  }

  search() {
    if (this.taskInfos) {
      const searchFilter = this.searchFilter ?? '';

      this.searchInfos = this.taskInfos.filter(taskInfo =>
        taskInfo.title?.includes(searchFilter) ||
        taskInfo.description?.includes(searchFilter));
    }
  }

  getTypeName(taskType?: TaskType): string {
    return TaskTypeMap.get(taskType);
  }

  getStatusName(taskStatus?: TaskStatus): string {
    return TaskStatusMap.get(taskStatus);
  }
}
