import { ModuleWithProviders, NgModule } from '@angular/core';

import { LoginComponent } from './login.component';
import { SharedModule } from '../shared/shared.module';
import { LoginService } from './login.service';

@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [LoginComponent],
  providers: [LoginService],
  exports: [LoginComponent]
})
export class LoginModule {
  static forRoot(): ModuleWithProviders<LoginModule>{
    return {
      ngModule: LoginModule,
      providers: [
        LoginService
      ]
    }
  }
}
