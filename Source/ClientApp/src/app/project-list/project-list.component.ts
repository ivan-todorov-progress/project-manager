import { Component, OnInit } from '@angular/core';
import { ProjectInfo } from '../app.model';
import { ProjectService } from '../app.services';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
})
export class ProjectListComponent implements OnInit {
  public projectInfos?: ProjectInfo[];

  constructor(private projectService: ProjectService) {
  }

  ngOnInit(): void {
    this.projectService.getProjects()
        .subscribe(result => this.projectInfos = result,
                   error => console.error(error));
  }
}
