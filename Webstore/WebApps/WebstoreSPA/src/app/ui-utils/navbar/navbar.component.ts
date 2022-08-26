import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IAppState } from 'src/app/shared/app-state/app-state';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';
import { Role } from 'src/app/shared/app-state/role';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  public appState$: Observable<IAppState>;

  constructor(private appStateService: AppStateService) {
    this.appState$ = this.appStateService.getAppState();
  }

  ngOnInit(): void {}

  public getNavbarTitle(appState: IAppState): string {
    if (appState.firstName !== undefined && appState.lastName !== undefined) {
      return `Welcome to Webstore, ${appState.firstName} ${appState.lastName}`;
    }
    return `Webstore`;
  }

  public canShowOrders(appState: IAppState): boolean {
    return appState.hasRole(Role.Buyer);
  }

  public canShowAdminPanel(appState: IAppState): boolean {
    return appState.hasRole(Role.Administrator);
  }

  public userLoggedIn(appState: IAppState): boolean {
    return !this.userLoggedOut(appState);
  }

  public userLoggedOut(appState: IAppState): boolean {
    return appState.isEmpty();
  }
}
