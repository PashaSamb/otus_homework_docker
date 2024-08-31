using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.Core.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IRepository<PromoCode> _promocodeRepository;

        public PromoCodeService(IRepository<PromoCode> promoCodeRepository)
        {
            _promocodeRepository = promoCodeRepository;
        }

        public async Task<IEnumerable<PromoCode>> GetAllPromoCodesAsync() => await _promocodeRepository.GetAllAsync();

        public async Task<PromoCode> GetPromoCodeByIdAsync(Guid id) => await _promocodeRepository.GetByIdAsync(id);

        public async Task<PromoCode> AddPromoCodeAsync(PromoCode item) => await _promocodeRepository.AddAsync(item);

        public async Task UpdatePromoCode(PromoCode item) => await _promocodeRepository.UpdateAsync(item);

        public async Task DeletePromoCode(PromoCode item) => await _promocodeRepository.DeleteAsync(item);
     
    }
}
