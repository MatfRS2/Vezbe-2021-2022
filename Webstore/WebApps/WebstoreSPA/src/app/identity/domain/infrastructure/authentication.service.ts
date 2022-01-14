import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ILoginRequest } from '../models/login-request';
import { ILoginResponse } from '../models/login-response';
import { ILogoutRequest } from '../models/logout-request';
import { IRefreshTokenRequest } from '../models/refresh-token-request';
import { IRefreshTokenResponse } from '../models/refresh-token-response';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private readonly url: string = 'http://localhost:4000/api/v1/Authentication';

  constructor(private httpClient: HttpClient) {}

  public login(request: ILoginRequest): Observable<ILoginResponse> {
    return this.httpClient.post<ILoginResponse>(`${this.url}/Login`, request);
  }

  public logout(request: ILogoutRequest): Observable<Object> {
    return this.httpClient.post(`${this.url}/Logout`, request);
  }

  public refreshToken(request: IRefreshTokenRequest): Observable<IRefreshTokenResponse> {
    return this.httpClient.post<IRefreshTokenResponse>(`${this.url}/Refresh`, request);
  }
}
