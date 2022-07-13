import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ProjectInfo } from '../app.model';
import { ProjectService, ImageService } from '../app.services';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
})
export class ProjectEditComponent implements OnInit {
  public projectId?: string;
  public projectInfo?: ProjectInfo;

  constructor(private location: Location,
              private activatedRoute: ActivatedRoute,
              private projectService: ProjectService,
              private imageService: ImageService) {
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

  upload(fileList: FileList): void {
    if (this.projectId && this.projectInfo) {
      const projectInfo = this.projectInfo;
      const imageFile = fileList.item(0);

      if (imageFile) {
        this.imageService.uploadImage(this.projectId, imageFile)
          .subscribe(result => projectInfo.image = result.url,
                     error => console.error(error))
      }
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
