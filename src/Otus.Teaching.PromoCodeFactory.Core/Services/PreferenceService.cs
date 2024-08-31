using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Services
{
    public class PreferenceService
    {
        private readonly IRepository<Preference> _preferenceRepository;

        public PreferenceService(IRepository<Preference> preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        public async Task<IEnumerable<Preference>> GetAllPreferencesAsync() => await _preferenceRepository.GetAllAsync();

        public async Task<Preference> GetPreferenceByIdAsync(Guid id) => await _preferenceRepository.GetByIdAsync(id);

        public async Task<Preference> AddPreferenceAsync(Preference item) => await _preferenceRepository.AddAsync(item);

        public async Task UpdatePreference(Preference item) => await _preferenceRepository.UpdateAsync(item);

        public async Task DeletePreference(Preference item) => await _preferenceRepository.DeleteAsync(item);

        public async Task<Preference> GetPreferenceByNameAsync(string name)
        {
            Expression<Func<Preference, bool>> expression = x => x.Name == name;
            var preferences = await _preferenceRepository.GetByExpressionAsync(expression);

            return preferences.FirstOrDefault();
        }

        public async Task<IEnumerable<Preference>> GetPreferencesAsync(List<Guid> preserencesId)
        {
            IEnumerable<Preference> preferences = new List<Preference>();
            if (preserencesId is { Count: > 0 })
            {
                Expression<Func<Preference, bool>> expression = x => preserencesId.Any(item => item == x.Id);
                return await _preferenceRepository.GetByExpressionAsync(expression);
            }

            return await Task.FromResult(preferences);
        }
    }
}
