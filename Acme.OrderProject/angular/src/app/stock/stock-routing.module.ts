import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from '@abp/ng.core';
import { StockComponent } from './stock.component';

const routes: Routes = [
  {
    path: '',
    component: StockComponent,
    // Remove permission guard so authenticated users can access the Stocks page
    canActivate: [authGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StockRoutingModule {}
