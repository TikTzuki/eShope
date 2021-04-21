import { DashboardComponent } from './dashboard.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardService } from './dasboard.service';

@NgModule({
  declarations: [DashboardComponent],
  imports: [ CommonModule ],
  providers: [DashboardService],
})
export class DashboardModule {}