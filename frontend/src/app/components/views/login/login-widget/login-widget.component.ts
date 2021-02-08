import { AfterContentInit, Component, EventEmitter, Output, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ILoginParameters } from './../../../../models/login-parameters';

@Component({
  selector: 'login-widget',
  templateUrl: './login-widget.component.html',
  styleUrls: ['./login-widget.component.scss'],
})
export class LoginWidgetComponent implements AfterContentInit {
  public username: string = '';
  public password: string = '';

  @Output() public tryToLogin: EventEmitter<ILoginParameters> = new EventEmitter();

  public loginForm = new FormGroup({
    username: new FormControl(this.username, [Validators.required]),
    password: new FormControl(this.password, [Validators.required]),
  });

  constructor(private renderer: Renderer2) {}

  ngAfterContentInit(): void {
    this.renderer.selectRootElement('#username').focus();
  }
}
