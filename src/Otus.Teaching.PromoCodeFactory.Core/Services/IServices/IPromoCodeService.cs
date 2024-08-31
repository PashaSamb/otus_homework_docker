using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Services.IServices
{
    public interface IPromoCodeService
    {
        Task<IEnumerable<PromoCode>> GetAllPromoCodesAsync();

        Task<PromoCode> GetPromoCodeByIdAsync(Guid id);

        Task<PromoCode> AddPromoCodeAsync(PromoCode item);

        Task UpdatePromoCode(PromoCode item);

        Task DeletePromoCode(PromoCode item);
    }
}
