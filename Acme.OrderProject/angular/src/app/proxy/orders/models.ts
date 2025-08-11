import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';

export interface AddOrderLineDto {
  orderId?: string;
  stockId?: string;
  quantity: number;
}

export interface CreateOrderDto {
  customerId?: string;
  orderDate?: string;
  deliveryAddress?: string;
}

export interface OrderDto extends AuditedEntityDto<string> {
  customerId?: string;
  orderDate?: string;
  deliveryAddress?: string;
  isApproved: boolean;
  totalAmount: number;
  lines: OrderLineDto[];
}

export interface OrderLineDto extends EntityDto<string> {
  stockId?: string;
  quantity: number;
  lineTotal: number;
}
