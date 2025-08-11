import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateStockDto {
  name: string;
  quantity: number;
  price: number;
}

export interface StockDto extends EntityDto<string> {
  name?: string;
  quantity: number;
  price: number;
}
