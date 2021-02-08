import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { accessTokenKey } from '../constants/constants';
import { LocalStorageService } from '../services/local-storage.service';
import { RedirectService } from '../services/redirect.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationGuard implements CanActivate {
  constructor(
    private localStorageService: LocalStorageService,
    private redirectService: RedirectService
  ) {}

  canActivate(
    _: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const token = this.localStorageService.get({ name: accessTokenKey });
    if (token) {
      return true;
    }

    this.redirectService.lastPath = state.url;
    this.redirectService.redirectToLogin();

    return false;
  }
}
