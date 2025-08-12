using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.OrderProject.Customers
{
    public class CustomerManager : DomainService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public virtual async Task<Customer> CreateAsync(string name, decimal riskLimit, string billAddress)
        {
            var entity = new Customer(GuidGenerator.Create())
            {
                Name = name,
                RiskLimit = riskLimit,
                BillAddress = billAddress
            };

            return await _customerRepository.InsertAsync(entity, autoSave: true);
        }

        public virtual async Task<Customer> UpdateAsync(Guid id, string name, decimal riskLimit, string billAddress)
        {
            var entity = await _customerRepository.GetAsync(id);

            entity.Name = name;
            entity.RiskLimit = riskLimit;
            entity.BillAddress = billAddress;

            return await _customerRepository.UpdateAsync(entity, autoSave: true);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            if (await _customerRepository.HasOrdersAsync(id))
            {
                throw new BusinessException("Cannot delete customer with existing orders.");
            }

            await _customerRepository.DeleteAsync(id, autoSave: true);
        }
    }
}