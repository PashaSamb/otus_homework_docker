using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.Core.Services;
using Otus.Teaching.PromoCodeFactory.Core.Services.IServices;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController: ControllerBase
    {
        private readonly IPromoCodeService _promoCodeService;

        private readonly PreferenceService _preferenceService;
        private readonly CustomerService _customerService;

        public PromocodesController( IPromoCodeService promoCodeService , PreferenceService preferenceService, CustomerService customerService)
        {
            _promoCodeService = promoCodeService;      
            _preferenceService = preferenceService;
            _customerService = customerService;
        }

        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            var promoCodes = await _promoCodeService.GetAllPromoCodesAsync();

            var promoCodeShortResponses = promoCodes.Select(CreatePromoCodeShortResponse).ToList();

            return Ok(promoCodeShortResponses);
        }

        private PromoCodeShortResponse CreatePromoCodeShortResponse(PromoCode promoCode)
        {
         
            return new PromoCodeShortResponse
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                ServiceInfo = promoCode.ServiceInfo,
                BeginDate = promoCode.StartDate.ToString("yyyy-MM-dd"),
                EndDate = promoCode.EndDate.ToString("yyyy-MM-dd"),
                PartnerName = promoCode.PartnerName
            };
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {

            var preference = await _preferenceService.GetPreferenceByNameAsync(request.Preference);

            if (preference == null)
            {
                return NotFound($"Preference '{request.Preference}' not found.");
            }

            var customers = await _customerService.GetCustomersWithPreferenceAsync(preference);

            var newpromoCode = new PromoCode()
            {
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                PartnerName = request.PartnerName
            };

            var promoCode = await _promoCodeService.AddPromoCodeAsync(newpromoCode);

            foreach (var customer in customers)
            {
                customer.PromoCodes.Add(promoCode);
                await _customerService.UpdateCustomer(customer);
            }

            return Ok();
        }
    }
}