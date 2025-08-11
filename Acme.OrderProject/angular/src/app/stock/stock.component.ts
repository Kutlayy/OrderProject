import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ListService, PagedResultDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

// Proxy yolunu projene göre ayarla: '@proxy/stocks' veya 'src/app/proxy/stocks'
import { StockService, StockDto, CreateUpdateStockDto } from '../proxy/stocks';

type PageEvent = { offset: number };
type SortEvent = { sorts?: { prop: string; dir: 'asc' | 'desc' }[] };
type StockFormModel = { id: string | null; name: string; quantity: number; price: number; };

@Component({
  standalone:false,
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  providers: [ListService],
})
export class StockComponent implements OnInit {
  @ViewChild('stockModal') stockModal!: TemplateRef<any>;
  private modalRef!: NgbModalRef;

  data: PagedResultDto<StockDto> = { items: [], totalCount: 0 };

  // ✅ Artık inputlar [(ngModel)] ile bağlanıyor; template tarafındaki kırmızılar kalkar
  formModel: StockFormModel = { id: null, name: '', quantity: 0, price: 0 };

  sorting = 'name';
  pageNumber = 0;
  pageSize = 10;
  isBusy = false;

  constructor(
    public readonly list: ListService,
    private readonly modal: NgbModal,
    private readonly confirm: ConfirmationService,
    private readonly stockService: StockService
  ) {}

  ngOnInit(): void {
    const queryFn = (q: PagedAndSortedResultRequestDto) => this.stockService.getList(q);
    this.list.hookToQuery(queryFn).subscribe(res => (this.data = res));
  }

  openCreate(): void {
    this.formModel = { id: null, name: '', quantity: 0, price: 0 };
    this.modalRef = this.modal.open(this.stockModal, { centered: true, size: 'md' });
  }

  openEdit(item: StockDto): void {
    this.formModel = {
      id: (item as any).id ?? null,
      name: item.name ?? '',
      quantity: (item as any).quantity ?? 0,
      price: item.price ?? 0,
    };
    this.modalRef = this.modal.open(this.stockModal, { centered: true, size: 'md' });
  }

  save(form: NgForm): void {
    if (form.invalid) return;
    this.isBusy = true;

    const dto: CreateUpdateStockDto = {
      name: this.formModel.name,
      quantity: this.formModel.quantity,
      price: this.formModel.price,
    };

    const id = this.formModel.id;

    const req = id ? this.stockService.update(id, dto) : this.stockService.create(dto);

    req.subscribe({
      next: () => {
        this.isBusy = false;
        this.modalRef.close();
        this.list.get(); // listeyi yenile
      },
      error: () => (this.isBusy = false),
    });
  }

  delete(item: StockDto): void {
    this.confirm.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(result => {
      if (result !== Confirmation.Status.confirm) return;
      this.stockService.delete((item as any).id).subscribe(() => this.list.get());
    });
  }

  onPage(e: PageEvent): void {
    this.pageNumber = e.offset;
    const input: PagedAndSortedResultRequestDto = {
      skipCount: this.pageNumber * this.pageSize,
      maxResultCount: this.pageSize,
      sorting: this.sorting,
    };
    this.list.get();
  }

  onSort(e: SortEvent): void {
    const s = e?.sorts?.[0];
    if (!s) return;
    this.sorting = `${s.prop} ${s.dir}`;
    const input: PagedAndSortedResultRequestDto = {
      skipCount: this.pageNumber * this.pageSize,
      maxResultCount: this.pageSize,
      sorting: this.sorting,
    };
    this.list.get();
  }
}
