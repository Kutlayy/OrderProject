import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard, permissionGuard } from '@abp/ng.core';
import { StockComponent } from './stock.component';

const routes: Routes = [
  {
    path: '',
    component: StockComponent,
    canActivate: [authGuard, permissionGuard],
    data: { requiredPolicy: 'OrderProject.Stocks' }, // backend permission: OrderProjectPermissions.Stocks.Default
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StockRoutingModule {}
