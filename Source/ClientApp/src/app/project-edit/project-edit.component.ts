import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ProjectInfo } from '../app.model';
import { ProjectService } from '../app.services';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
})
export class ProjectEditComponent implements OnInit {
  public projectId?: string;
  public projectInfo?: ProjectInfo;

  constructor(private location: Location,
              private activatedRoute: ActivatedRoute,
              private projectService: ProjectService) {
    const projectId = this.activatedRoute.snapshot.paramMap.get('projectId');

    if (projectId != null) {
      this.projectId = projectId;
    }
  }

  ngOnInit(): void {
    if (this.projectId) {
      this.projectService.getProject(this.projectId)
          .subscribe(result => this.projectInfo = result,
                     error => console.error(error));
    } else {
      this.projectInfo = new ProjectInfo();
    }
  }

  save(): void {
    if (this.projectInfo) {
      if (this.projectId) {
        this.projectService.updateProject(this.projectInfo)
            .subscribe(result => this.location.back(),
                       error => console.error(error));
      } else {
        this.projectService.createProject(this.projectInfo)
            .subscribe(result => this.location.back(),
                       error => console.error(error));
      }
    }
  }

  cancel(): void {
    this.location.back();
  }
}
