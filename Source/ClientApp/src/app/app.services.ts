import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProjectInfo, TaskInfo, ChangeSet, ImageInfo } from './app.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl + 'project';
  }

  getProjects(): Observable<ProjectInfo[]> {
    return this.http.get<ProjectInfo[]>(this.baseUrl);
  }

  getProject(projectId: string): Observable<ProjectInfo> {
    return this.http.get<ProjectInfo>(`${this.baseUrl}/${projectId}`);
  }

  createProject(projectInfo: ProjectInfo): Observable<ProjectInfo> {
    return this.http.put<ProjectInfo>(this.baseUrl, projectInfo);
  }

  updateProject(projectInfo: ProjectInfo): Observable<ProjectInfo> {
    return this.http.post<ProjectInfo>(this.baseUrl, projectInfo);
  }
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl + 'task';
  }

  getTasks(projectId: string): Observable<TaskInfo[]> {
    return this.http.get<TaskInfo[]>(`${this.baseUrl}/${projectId}`);
  }

  getTask(projectId: string, taskId: string): Observable<TaskInfo> {
    return this.http.get<TaskInfo>(`${this.baseUrl}/${projectId}/${taskId}`);
  }

  createTask(projectId: string, taskInfo: TaskInfo): Observable<TaskInfo> {
    return this.http.put<TaskInfo>(`${this.baseUrl}/${projectId}`, taskInfo);
  }

  updateTask(projectId: string, taskInfo: TaskInfo): Observable<TaskInfo> {
    return this.http.post<TaskInfo>(`${this.baseUrl}/${projectId}`, taskInfo);
  }
}

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl + 'history';
  }

  getChanges(parentId: string): Observable<ChangeSet[]> {
    return this.http.get<ChangeSet[]>(`${this.baseUrl}/${parentId}`);
  }
}

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl + 'image';
  }

  uploadImage(projectId: string, imageFile: File): Observable<ImageInfo> {
    const formData = new FormData();
    const imageName = imageFile.name;

    formData.append('Image', imageFile, imageName);

    return this.http.post<ImageInfo>(`${this.baseUrl}/${projectId}`, formData);
  }
}
