import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';

import { StockRoutingModule } from './stock-routing.module';
import { StockComponent } from './stock.component';

@NgModule({
  declarations: [StockComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,   // ⬅️ BUNUN OLMASI ŞART
    NgbModalModule,
    NgxDatatableModule,
    CoreModule,
    ThemeSharedModule,     // ⬅️ abpLocalization pipe için
    StockRoutingModule,
  ],
})
export class StockModule {}
