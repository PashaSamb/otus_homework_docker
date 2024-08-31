using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Services;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferencesController : ControllerBase
    {
        private readonly PreferenceService _preferenceService;

        public PreferencesController(PreferenceService preferenceService )
        {
           _preferenceService = preferenceService;
        }


        /// <summary>
        /// Получить список предпочтений.
        /// </summary>
        /// <returns>Список предпочтений</returns>

        [HttpGet]
        public async Task<List<PreferenceResponse>> GetPreferencesAsync()
        {
            var preferences = await _preferenceService.GetAllPreferencesAsync();       
            return preferences.Select(x => new PreferenceResponse { Id = x.Id, Name = x.Name }).ToList();
        }

    }
}
