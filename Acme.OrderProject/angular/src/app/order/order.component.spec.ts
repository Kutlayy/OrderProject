import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { of } from 'rxjs';

import { OrderComponent } from './order.component';
import { ListService } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { OrderService } from '../proxy/orders';
import { CustomerService } from '../proxy/customers';
import { StockService } from '../proxy/stocks';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

describe('OrderComponent', () => {
  let component: OrderComponent;
  let fixture: ComponentFixture<OrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrderComponent],
      imports: [FormsModule],
      providers: [
        { provide: ListService, useValue: { hookToQuery: () => of({ items: [], totalCount: 0 }), get: () => {} } },
        { provide: OrderService, useValue: { getList: () => of({ items: [], totalCount: 0 }), create: () => of({}), addLine: () => of({}), approve: () => of({}), delete: () => of({}) } },
        { provide: CustomerService, useValue: { getList: () => of({ items: [] }) } },
        { provide: StockService, useValue: { getList: () => of({ items: [] }) } },
        { provide: NgbModal, useValue: { open: () => ({ close() {}, dismiss() {} }) } },
        { provide: ConfirmationService, useValue: { warn: () => of(Confirmation.Status.confirm) } },
      ],
      schemas: [NO_ERRORS_SCHEMA],
    }).compileComponents();

    fixture = TestBed.createComponent(OrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
