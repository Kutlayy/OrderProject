import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { RoutesService, eLayoutType } from '@abp/ng.core';

@NgModule({
  declarations: [CustomerComponent],
  imports: [
    SharedModule,
    CustomerRoutingModule,
    NgbDatepickerModule,
    NgxDatatableModule,
  ],
})
export class CustomerModule {
  constructor(routes: RoutesService) {
    routes.add([
      {
        path: '/customers',
        name: '::Menu:Customers',
        iconClass: 'fas fa-users',
        layout: eLayoutType.application,
      },
    ]);
  }
}
