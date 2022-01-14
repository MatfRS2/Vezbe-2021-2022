import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, switchMap, take } from 'rxjs';
import { AppStateService } from '../app-state/app-state.service';
import { IAppState } from '../app-state/app-state';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
  private readonly whitelistUrls: string[] = ['/api/v1/Authentication/Login'];

  constructor(private appStateService: AppStateService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (this.isWhitelisted(request.url)) {
      return next.handle(request);
    }

    return this.appStateService.getAppState().pipe(
      take(1),
      switchMap((appState: IAppState) => {
        if (appState.accessToken !== undefined) {
          request = this.addToken(request, appState.accessToken);
        }

        return next.handle(request);
      })
    );
  }

  private isWhitelisted(url: string): boolean {
    return this.whitelistUrls.some((whitelistedUrl: string) => url.includes(whitelistedUrl));
  }

  private addToken(request: HttpRequest<unknown>, accessToken: string): HttpRequest<unknown> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`,
      },
    });
  }
}
