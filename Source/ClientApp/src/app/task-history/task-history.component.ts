import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChangeSet } from '../app.model';
import { NavMenuItem, menuItems } from '../nav-menu/nav-menu.component';
import { HistoryService } from '../app.services';

@Component({
  selector: 'app-task-history',
  templateUrl: './task-history.component.html',
})
export class TaskHistoryComponent implements OnInit, OnDestroy {
  public projectId?: string;
  public taskId?: string;
  public changeSets?: ChangeSet[];

  constructor(private activatedRoute: ActivatedRoute,
              private historyService: HistoryService) {
    const projectId = this.activatedRoute.snapshot.paramMap.get('projectId');
    const taskId = this.activatedRoute.snapshot.paramMap.get('taskId');

    if (projectId != null) {
      this.projectId = projectId;
    }

    if (taskId != null) {
      this.taskId = taskId;
    }
  }

  ngOnInit(): void {
    if (this.projectId) {
      const menuItem = new NavMenuItem(`/task-list/${this.projectId}`, 'Tasks');

      menuItems.push(menuItem);
    }

    if (this.taskId) {
      this.historyService.getChanges(this.taskId)
          .subscribe(result => this.changeSets = result,
                     error => console.error(error));
    }
  }

  ngOnDestroy(): void {
    if (this.projectId) {
      menuItems.pop();
    }
  }
}
