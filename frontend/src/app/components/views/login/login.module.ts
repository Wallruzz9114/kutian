import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../../../shared/shared.module';
import { LoginWidgetComponent } from './login-widget/login-widget.component';
import { LoginPageComponent } from './login-page/login-page.component';

@NgModule({
  declarations: [LoginWidgetComponent, LoginPageComponent],
  imports: [CommonModule, SharedModule, FormsModule, ReactiveFormsModule],
})
export class LoginModule {}
