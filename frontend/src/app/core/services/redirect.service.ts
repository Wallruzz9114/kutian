import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class RedirectService {
  constructor(private router: Router) {}

  public loginURL = '/login';
  public lastPath: string = '';
  public defaultPath = '/';

  setLoginUrl(value: string): void {
    this.loginURL = value;
  }

  setDefaultUrl(value: string): void {
    this.defaultPath = value;
  }

  public redirectToLogin(): void {
    this.router.navigate([this.loginURL]);
  }

  public redirectPreLogin(): void {
    if (this.lastPath && this.lastPath !== this.loginURL) {
      this.router.navigate([this.lastPath]);
      this.lastPath = '';
    } else {
      this.router.navigate([this.defaultPath]);
    }
  }
}
