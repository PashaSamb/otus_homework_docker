using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.Core.Services;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly PreferenceService _preferenceService;

        public CustomersController(CustomerService customerService, PreferenceService  preferenceService)
        {
            _customerService = customerService;
            _preferenceService = preferenceService;
        }

        /// <summary>
        ///  Получение списка пользователей
        /// </summary>
      
        [HttpGet]
        public async Task<ActionResult<CustomerShortResponse>> GetCustomersAsync()
        {
           
            var data = await _customerService.GetAllCustomersAsync();

            var response = data.Select(x => new CustomerShortResponse()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
            }).ToList();

            return Ok(response);
        }

        /// <summary>
        /// Получение пользователя по id
        /// </summary>
        /// <param name="id"></param>

        
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            var data = await _customerService.GetCustomerByIdAsync(id);
            if (data == null)
            {
                return NotFound($"Customer with : {id} is not found");
            }

            var response = new CustomerResponse()
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                PromoCodes = data.PromoCodes.Select(x => new PromoCodeShortResponse()
                {
                    Id = x.Id,
                    Code = x.Code,
                    BeginDate = x.StartDate.ToLongDateString(),
                    EndDate = x.EndDate.ToLongDateString()
                }).ToList(),

                Preferences = data.Preferences.Select(x => new PreferenceResponse()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return Ok(response);
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <param name="request"></param>

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            // Create or add preferences
            var preferences = await _preferenceService.GetPreferencesAsync(request.PreferenceIds);

            foreach (var preference in preferences)
            {
                customer.Preferences.Add(preference);
            }

            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound($"Customer not found with id: {id}");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;

            // Update or add preferences
            var existingPreferences = customer.Preferences;
            var newPreferences = await _preferenceService.GetPreferencesAsync(request.PreferenceIds);

            foreach (var preference in newPreferences)
            {
                if (!existingPreferences.Any(e => e.Id == preference.Id))
                {
                    customer.Preferences.Add(preference);
                }
            }

            if (id != customer.Id)
            {
                return BadRequest("This customer cannot be modified");
            }

            await _customerService.UpdateCustomer(customer);
            return NoContent();
        }
        
        /// <summary>
        /// Удалить пользователя по id
        /// </summary>
        /// <param name="id"></param>

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound($"No customer has been found for id: {id}");

            await _customerService.DeleteCustomer(customer);
            return NoContent();
        }
    }
}