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
        private readonly CustomerManager _customerManager;

        public CustomerAppService(
            ICustomerRepository customerRepository,
            CustomerManager customerManager)
        {
            _customerRepository = customerRepository;
            _customerManager = customerManager;
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
            var customer = await _customerManager.UpdateAsync(
                id,
                input.Name,
                input.RiskLimit,
                input.BillAddress);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _customerManager.DeleteAsync(id);
        }

        public async Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            var entity = await _customerManager.CreateAsync(
                input.Name,
                input.RiskLimit,
                input.BillAddress);

            return ObjectMapper.Map<Customer, CustomerDto>(entity);
        }

    }
}