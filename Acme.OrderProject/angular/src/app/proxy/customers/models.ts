import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateCustomerDto {
  name: string;
  riskLimit: number;
  billAddress: string;
}

export interface CustomerDto extends AuditedEntityDto<string> {
  name?: string;
  riskLimit: number;
  billAddress?: string;
}
