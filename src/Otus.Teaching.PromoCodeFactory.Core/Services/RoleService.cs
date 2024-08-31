using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.RoleFactory.Core.Services
{
    public class RoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync() => await _roleRepository.GetAllAsync();

        public async Task<Role> GetRoleByIdAsync(Guid id) => await _roleRepository.GetByIdAsync(id);

        public async Task<Role> AddRoleAsync(Role item) => await _roleRepository.AddAsync(item);

        public async Task UpdateRole(Role item) => await _roleRepository.UpdateAsync(item);

        public async Task DeleteRole(Role item) => await _roleRepository.DeleteAsync(item);
    }
}
