using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.OrderProject.Customers
{
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerAppService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task<PagedResultDto<CustomerDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var customers = await _customerRepository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting ?? nameof(Customer.Name)
            );

            var totalCount = await _customerRepository.GetCountAsync();

            return new PagedResultDto<CustomerDto>(
                totalCount,
                ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers)
            );
        }

        public async Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input)
        {
            var customer = await _customerRepository.GetAsync(id);

            customer.Name = input.Name;
            customer.RiskLimit = input.RiskLimit;
            customer.BillAddress = input.BillAddress;

            await _customerRepository.UpdateAsync(customer, autoSave: true);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            var hasOrders = await _customerRepository.HasOrdersAsync(id);

            if (hasOrders)
            {
                throw new BusinessException("Cannot delete customer with existing orders.");
            }

            await _customerRepository.DeleteAsync(id);
        }

        public async Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            var entity = ObjectMapper.Map<CreateUpdateCustomerDto, Customer>(input);

            // anında kaydetmek için autoSave: true
            entity = await _customerRepository.InsertAsync(entity, autoSave: true);

            return ObjectMapper.Map<Customer, CustomerDto>(entity);
        }

    }
}
