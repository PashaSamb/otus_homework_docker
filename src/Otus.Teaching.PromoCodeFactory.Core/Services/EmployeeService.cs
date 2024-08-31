using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Services
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployiesAsync() => await _employeeRepository.GetAllAsync();

        public async Task<Employee> GetEmployeeByIdAsync(Guid id) => await _employeeRepository.GetByIdAsync(id);

        public async Task<Employee> AddEmployeeAsync(Employee item) => await _employeeRepository.AddAsync(item);

        public async Task UpdateEmployee(Employee item) => await _employeeRepository.UpdateAsync(item);

        public async Task DeleteEmployee(Employee item) => await _employeeRepository.DeleteAsync(item);
    }
}
