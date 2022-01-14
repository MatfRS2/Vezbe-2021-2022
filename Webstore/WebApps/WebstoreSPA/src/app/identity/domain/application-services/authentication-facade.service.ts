import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, switchMap } from 'rxjs';
import { AppStateService } from 'src/app/shared/app-state/app-state.service';
import { JwtPayloadKeys } from 'src/app/shared/jwt/jwt-payload-keys';
import { JwtService } from 'src/app/shared/jwt/jwt.service';
import { AuthenticationService } from '../infrastructure/authentication.service';
import { ILoginRequest } from '../models/login-request';
import { ILoginResponse } from '../models/login-response';
import { IUserDetails } from '../models/user-details';
import { UserFacadeService } from './user-facade.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationFacadeService {
  constructor(
    private authenticationService: AuthenticationService,
    private appStateService: AppStateService,
    private jwtService: JwtService,
    private userService: UserFacadeService
  ) {}

  public login(username: string, password: string): Observable<boolean> {
    const request: ILoginRequest = { username, password };

    return this.authenticationService.login(request).pipe(
      switchMap((loginResponse: ILoginResponse) => {
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);
        this.appStateService.setRoles(payload[JwtPayloadKeys.Role]);

        return this.userService.getUserDetails(payload[JwtPayloadKeys.Username]);
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setFirstName(userDetails.firstName);
        this.appStateService.setLastName(userDetails.lastName);
        this.appStateService.setUserId(userDetails.id);

        return true;
      }),
      catchError((err) => {
        console.log(err);
        return of(false);
      })
    );
  }
}
