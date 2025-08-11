import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { RoutesService, eLayoutType } from '@abp/ng.core';

import { OrderRoutingModule } from './order-routing.module';
import { OrderComponent } from './order.component';

@NgModule({
  declarations: [OrderComponent],
  imports: [
    CommonModule,
    SharedModule,
    OrderRoutingModule,
    NgbDatepickerModule,
    NgxDatatableModule,
  ],
})
export class OrderModule {
  constructor(routes: RoutesService) {
    routes.add([
      {
        path: '/orders',
        name: '::Menu:Orders',
        iconClass: 'fas fa-shopping-cart',
        layout: eLayoutType.application,
      },
    ]);
  }
}
