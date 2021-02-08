import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AuthenticationGuard } from './guards/authentication.guard';
import { HeadersInterceptor } from './interceptors/headers.interceptor';
import { JwtInterceptor } from './interceptors/jwt-interceptor';
import { AuthenticationService } from './services/authenication.service';
import { LocalStorageService } from './services/local-storage.service';
import { RedirectService } from './services/redirect.service';

@NgModule({
  declarations: [],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HeadersInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
    AuthenticationGuard,
    AuthenticationService,
    LocalStorageService,
    RedirectService,
  ],
  imports: [CommonModule, HttpClientModule],
})
export class CoreModule {}
