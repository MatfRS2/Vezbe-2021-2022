import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable, take } from 'rxjs';
import { IAppState } from '../app-state/app-state';
import { AppStateService } from '../app-state/app-state.service';

@Injectable({
  providedIn: 'root',
})
export class NotAuthenticatedGuard implements CanActivate {
  public constructor(private appStateService: AppStateService, private routerService: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        if (!appState.isEmpty()) {
          return true;
        }
        return this.routerService.createUrlTree(['/identity', 'login']);
      })
    );
  }
}
