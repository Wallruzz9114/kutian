import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { accessTokenKey } from '../constants/constants';
import { LocalStorageService } from '../services/local-storage.service';
import { RedirectService } from '../services/redirect.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(
    private localStorageService: LocalStorageService,
    private redirectService: RedirectService
  ) {}

  public intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap(
        (httpEvent: HttpEvent<any>) => httpEvent,
        (error) => {
          if (error instanceof HttpErrorResponse && error.status === 401) {
            this.localStorageService.put({ name: accessTokenKey, value: null });
            this.redirectService.redirectToLogin();
          }
        }
      )
    );
  }
}
