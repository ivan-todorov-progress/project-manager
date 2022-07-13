import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskInfo, TaskType, TaskTypeMap, TaskStatus, TaskStatusMap } from '../app.model';
import { NavMenuItem, menuItems } from '../nav-menu/nav-menu.component';
import { TaskService } from '../app.services';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
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
    if (this.taskInfos && this.searchFilter) {
      const searchFilter = this.searchFilter.toLowerCase();

      this.searchInfos = this.taskInfos.filter(taskInfo => {
        const title = taskInfo.title?.toLowerCase();
        const description = taskInfo.description?.toLowerCase();

        return title?.includes(searchFilter) ||
               description?.includes(searchFilter);
      });
    } else {
      this.searchInfos = this.taskInfos;
    }
  }

  getTypeName(taskType?: TaskType): string {
    return TaskTypeMap.get(taskType);
  }

  getStatusName(taskStatus?: TaskStatus): string {
    return TaskStatusMap.get(taskStatus);
  }
}
