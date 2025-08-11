import type { CreateUpdateStockDto, StockDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StockService {
  apiName = 'Default';
  

  create = (input: CreateUpdateStockDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StockDto>({
      method: 'POST',
      url: '/api/app/stock',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/stock/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StockDto>({
      method: 'GET',
      url: `/api/app/stock/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<StockDto>>({
      method: 'GET',
      url: '/api/app/stock',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateStockDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StockDto>({
      method: 'PUT',
      url: `/api/app/stock/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
