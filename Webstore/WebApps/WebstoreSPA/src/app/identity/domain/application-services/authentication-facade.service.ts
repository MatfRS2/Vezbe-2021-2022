import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';
import { AuthenticationService } from '../infrastructure/authentication.service';
import { ILoginRequest } from '../models/login-request';
import { ILoginResponse } from '../models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationFacadeService {
  constructor(private authenticationService: AuthenticationService, private appStateService: AppStateService) { }

  public login(username: string, password: string): Observable<boolean> {
    const request: ILoginRequest = { username, password };

    return this.authenticationService.login(request).pipe(
      map((loginResponse: ILoginResponse) => {
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        return true;
      }),
      catchError((err) => {
        console.log(err);
        return of(false);
      })
    );
  }
}
