import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { OrderService, OrderDto, CreateOrderDto, AddOrderLineDto } from '../proxy/orders';
import { CustomerService, CustomerDto } from '../proxy/customers';
import { StockService, StockDto } from '../proxy/stocks';
import { ConfirmationService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-order',
  standalone: false,
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  providers: [ListService],
})
export class OrderComponent implements OnInit {
  orders: OrderDto[] = [];
  totalCount = 0;

  customers: CustomerDto[] = [];
  customerMap = new Map<string, string>();
  stocks: StockDto[] = [];

  orderForm: CreateOrderDto = {} as any;
  lineForm: AddOrderLineDto = { quantity: 1 } as any;
  selectedOrder?: OrderDto;

  @ViewChild('orderModal') orderModal!: TemplateRef<any>;
  @ViewChild('lineModal') lineModal!: TemplateRef<any>;

  constructor(
    public readonly list: ListService,
    private orderService: OrderService,
    private customerService: CustomerService,
    private stockService: StockService,
    private modal: NgbModal,
    private confirm: ConfirmationService,
  ) {}

  ngOnInit() {
    this.customerService.getList({ skipCount: 0, maxResultCount: 100 }).subscribe(res => {
      this.customers = res.items;
      this.customerMap = new Map(res.items.map(c => [c.id!, c.name!]));
    });

    this.stockService.getList({ skipCount: 0, maxResultCount: 100 }).subscribe(res => (this.stocks = res.items));

    this.list.hookToQuery(query => this.orderService.getList(query)).subscribe((res: PagedResultDto<OrderDto>) => {
      this.orders = res.items;
      this.totalCount = res.totalCount;
    });
  }

  customerName(id?: string) {
    return (id && this.customerMap.get(id)) || '—';
  }

  openCreate() {
    this.orderForm = {
      customerId: undefined as any,
      orderDate: new Date().toISOString().slice(0, 10) as any,
      deliveryAddress: ''
    } as any;
    this.lineForm = { quantity: 1 } as any;
    this.modal.open(this.orderModal);
  }

  saveOrder(modalRef: any) {
    this.orderForm.deliveryAddress = (this.orderForm.deliveryAddress as any || '').trim() as any;

    this.orderService.create(this.orderForm).subscribe(order => {
      if (this.lineForm.stockId) {
        this.orderService.addLine({ ...this.lineForm, orderId: order.id }).subscribe(() => {
          this.list.get();
          modalRef.close();
        });
      } else {
        this.list.get();
        modalRef.close();
      }
    });
  }

  approve(order: OrderDto) {
    this.orderService.approve(order.id).subscribe(() => this.list.get());
  }

  openAddLine(order: OrderDto) {
    this.selectedOrder = order;
    this.lineForm = { orderId: order.id, stockId: undefined, quantity: 1 } as any;
    this.modal.open(this.lineModal);
  }

  saveLine(modalRef: any) {
    this.orderService.addLine(this.lineForm).subscribe(() => {
      this.list.get();
      modalRef.close();
    });
  }

  delete(id: string) {
    this.orderService.delete(id).subscribe(() => this.list.get());
  }
}
