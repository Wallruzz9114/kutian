import { Component } from '@angular/core';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'main-container',
  templateUrl: './main-container.component.html',
  styleUrls: ['./main-container.component.scss'],
})
export class MainContainerComponent {
  public isAdmin$: Observable<boolean> = of(false);
}
