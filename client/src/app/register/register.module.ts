import { SharedModule } from '../shared/shared.module';
import { RegisterComponent } from '../register/register.component';
import { RegisterService } from '../register/register.service';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ModuleWithProviders, NgModule } from '@angular/core';

@NgModule({
  imports: [SharedModule, FontAwesomeModule],
  declarations: [RegisterComponent],
  providers: [RegisterService],
  exports: [RegisterComponent]
})
export class RegisterModule{

  static forRoot(): ModuleWithProviders<RegisterModule>{
    return {
      ngModule: RegisterModule,
      providers: [
        RegisterService
      ]
    }
  }
}
