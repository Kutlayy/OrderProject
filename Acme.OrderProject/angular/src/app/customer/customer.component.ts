import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { CustomerService, CustomerDto, CreateUpdateCustomerDto } from '../proxy/customers';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone:false,
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  providers: [ListService]
})
export class CustomerComponent implements OnInit {
  customers: CustomerDto[] = [];
  totalCount = 0;
  selectedCustomer?: CustomerDto;
  formModel: CreateUpdateCustomerDto = {} as any;

  @ViewChild('customerModal') customerModal!: TemplateRef<any>;

  constructor(
    public readonly list: ListService,
    private customerService: CustomerService,
    private modalService: NgbModal
  ) {}

  ngOnInit() {
    this.list.hookToQuery(query => this.customerService.getList(query))
      .subscribe((res: PagedResultDto<CustomerDto>) => {
        this.customers = res.items;
        this.totalCount = res.totalCount;
      });
  }

  openCreateModal() {
    this.selectedCustomer = undefined;
    this.formModel = {} as any;
    this.modalService.open(this.customerModal);
  }

  openEditModal(customer: CustomerDto) {
    this.selectedCustomer = customer;
    this.formModel = {
      name: customer.name,
      riskLimit: customer.riskLimit,
      billAddress: customer.billAddress
    };
    this.modalService.open(this.customerModal);
  }

  save(modal: any) {
    if (this.selectedCustomer) {
      this.customerService.update(this.selectedCustomer.id, this.formModel).subscribe(() => {
        this.list.get();
        modal.close();
      });
    } else {
      this.customerService.create(this.formModel).subscribe(() => {
        this.list.get();
        modal.close();
      });
    }
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerService.delete(id).subscribe(() => this.list.get());
    }
  }
}
