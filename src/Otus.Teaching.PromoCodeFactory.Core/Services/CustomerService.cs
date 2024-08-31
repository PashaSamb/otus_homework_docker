using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Services
{
    public class CustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() => await _customerRepository.GetAllAsync();

        public async Task<Customer> GetCustomerByIdAsync(Guid id) =>  await _customerRepository.GetByIdAsync(id);
           
        public async Task<Customer> AddCustomerAsync(Customer item) => await _customerRepository.AddAsync(item);

        public async Task UpdateCustomer(Customer item) => await _customerRepository.UpdateAsync(item);

        public async Task DeleteCustomer(Customer item) => await _customerRepository.DeleteAsync(item);

        public async Task<IEnumerable<Customer>> GetCustomersWithPreferenceAsync(Preference preference)
        {
            if (preference == null)
            {
                return [];
            }

            Expression<Func<Customer, bool>> expression = customer =>
                customer.Preferences.Any(p => p.Id == preference.Id);

            return await _customerRepository.GetByExpressionAsync(expression);
        }
    }
}
