import { ProductListComponent } from './product-list/product-list.component';
import { ProductComponent } from './product/product.component';
import { OrderListComponent } from './order-list/order-list.component';
import { AnalysisComponent } from './analysis/analysis.component';
import { ProfileComponent } from './profile/profile.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterModule, Routes } from "@angular/router";
import { HaNoiTowerComponent } from './ha-noi-tower/ha-noi-tower.component';
import { TaciComponent } from './taci/taci.component';


export const routes: Routes = [
  {path:'', redirectTo: 'dashboard',  pathMatch: 'full', data: null},
  {path:'dashboard', component: DashboardComponent, data: {name: 'Dashboard', icon:'fas fa-tachometer-alt'}},
  { path: 'products', component: ProductListComponent, data: { name: 'Product', icon:'fas fa-cube' } },
  { path: 'product-new', component: ProductComponent, data: { name: 'AddProduct', icon:'fas fa-cube' } },
  { path: 'product/:productId', component: ProductComponent, data: null },
  { path: 'orders', component: OrderListComponent, data: { name: 'Orders', icon:'fas fa-file-invoice' } },
  { path: 'analysis', component: AnalysisComponent, data: { name: 'Analysis', icon:'fas fa-chart-line' } },
  { path: 'profile', component: ProfileComponent, data: { name: 'Profile', icon: 'fas fa-address-card' } },
  { path: 'hanoi-tower', component: HaNoiTowerComponent, data: null },
  { path: 'taci', component: TaciComponent, data: null },
]

export const routing = RouterModule.forRoot(routes);