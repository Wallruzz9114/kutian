import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { MainContainerComponent } from './components/common/main-container/main-container.component';
import { MainComponent } from './components/common/main/main.component';
import { LoginModule } from './components/views/login/login.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [MainComponent, MainContainerComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    CoreModule,
    LoginModule,
  ],
  providers: [],
  bootstrap: [MainContainerComponent],
})
export class AppModule {}
