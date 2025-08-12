import {
  Component,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import {
  ListService,
  PagedResultDto,
} from '@abp/ng.core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  OrderService,
  OrderDto,
  CreateOrderDto,
  AddOrderLineDto,
} from '../proxy/orders';
import { CustomerService, CustomerDto } from '../proxy/customers';
import { StockService, StockDto } from '../proxy/stocks';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

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
    this.customerService
      .getList({ skipCount: 0, maxResultCount: 100 })
      .subscribe((res) => (this.customers = res.items));
    this.stockService
      .getList({ skipCount: 0, maxResultCount: 100 })
      .subscribe((res) => (this.stocks = res.items));

    this.list
      .hookToQuery((query) => this.orderService.getList(query))
      .subscribe((res: PagedResultDto<OrderDto>) => {
        this.orders = res.items;
        this.totalCount = res.totalCount;
      });
  }

  openCreate() {
    this.orderForm = {} as any;
    this.lineForm = { quantity: 1 } as any;
    this.modal.open(this.orderModal);
  }

  saveOrder(modalRef: any) {
    this.orderService.create(this.orderForm).subscribe((order) => {
      if (this.lineForm.stockId) {
        this.orderService
          .addLine({ ...this.lineForm, orderId: order.id })
          .subscribe(() => {
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
    this.lineForm = { orderId: order.id, stockId: undefined, quantity: 1 };
    this.modal.open(this.lineModal);
  }

  saveLine(modalRef: any) {
    this.orderService.addLine(this.lineForm).subscribe(() => {
      this.list.get();
      modalRef.close();
    });
  }
}
