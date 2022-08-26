import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable, of, switchMap } from 'rxjs';
import { IAppState } from '../app-state/app-state';
import { AppStateService } from '../app-state/app-state.service';
import { Role } from '../app-state/role';
import { NotAuthenticatedGuard } from './not-authenticated.guard';

@Injectable({
  providedIn: 'root',
})
export class RoleGuard implements CanActivate {
  public constructor(private appStateService: AppStateService, private notAuthenticatedGuard: NotAuthenticatedGuard, private routerService: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return (this.notAuthenticatedGuard.canActivate(route, state) as Observable<boolean | UrlTree>).pipe(
      switchMap((result: boolean | UrlTree) => {
        if (result instanceof UrlTree) {
          return of(result);
        }
        return this.appStateService.getAppState();
      }),
      map((result: UrlTree | IAppState) => {
        if (result instanceof UrlTree) {
          return result;
        }
        if (result.hasRole(Role.Administrator)) {
          return true;
        }
        return this.routerService.createUrlTree(['/identity', 'login']);
      })
    );
  }
}
