import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { accessTokenKey } from '../constants/constants';
import { LocalStorageService } from '../services/local-storage.service';

@Injectable()
export class HeadersInterceptor implements HttpInterceptor {
  constructor(private localStorageService: LocalStorageService) {}

  public intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const token = this.localStorageService.get({ name: accessTokenKey }) || '';
    return next.handle(
      request.clone({
        headers: request.headers.set('Authorization', `Bearer ${token}`),
      })
    );
  }
}
