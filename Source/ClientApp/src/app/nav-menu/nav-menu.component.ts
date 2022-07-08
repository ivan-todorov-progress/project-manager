import { Component } from '@angular/core';

export class NavMenuItem {
  constructor(public path: string, public name: string) {
  }
}

export const menuItems = [
  new NavMenuItem('/', 'Home'),
  new NavMenuItem('/project-list', 'Projects')
];

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public menuItems: NavMenuItem[];
  public isExpanded: boolean;

  constructor() {
    this.menuItems = menuItems;
    this.isExpanded = false;
  }

  expand() {
    this.isExpanded = true;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
