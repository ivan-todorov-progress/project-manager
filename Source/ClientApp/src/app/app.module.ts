import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectEditComponent } from './project-edit/project-edit.component';
import { TaskListComponent } from './task-list/task-list.component';
import { TaskInfoComponent } from './task-info/task-info.component';
import { TaskEditComponent } from './task-edit/task-edit.component';
import { TaskBoardComponent } from './task-board/task-board.component';
import { TaskHistoryComponent } from './task-history/task-history.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProjectListComponent,
    ProjectEditComponent,
    TaskListComponent,
    TaskInfoComponent,
    TaskEditComponent,
    TaskBoardComponent,
    TaskHistoryComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'project-list', component: ProjectListComponent, pathMatch: 'full' },
      { path: 'project-new', component: ProjectEditComponent, pathMatch: 'full' },
      { path: 'project-edit/:projectId', component: ProjectEditComponent, pathMatch: 'full' },
      { path: 'task-list/:projectId', component: TaskListComponent, pathMatch: 'full' },
      { path: 'task-new/:projectId', component: TaskEditComponent, pathMatch: 'full' },
      { path: 'task-edit/:projectId/:taskId', component: TaskEditComponent, pathMatch: 'full' },
      { path: 'task-board/:projectId', component: TaskBoardComponent, pathMatch: 'full' },
      { path: 'task-history/:projectId/:taskId', component: TaskHistoryComponent, pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
