import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AuthenticationService } from './../../../../core/services/authenication.service';
import { ILoginParameters } from './../../../../models/login-parameters';

@Component({
  selector: 'login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent implements OnDestroy {
  private readonly _destroyed: Subject<void> = new Subject();

  constructor(private authenicationService: AuthenticationService, private router: Router) {}

  public tryToLogin($event: ILoginParameters) {
    this.authenicationService
      .tryToLogin($event)
      .pipe(takeUntil(this._destroyed))
      .subscribe(
        () => this.router.navigateByUrl('/'),
        (errorResponse) => console.log(errorResponse)
      );
  }

  ngOnDestroy(): void {
    this._destroyed.next();
    this._destroyed.complete();
  }
}
