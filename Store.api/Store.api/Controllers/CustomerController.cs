using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Store.Models.Models.Customer;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerService = serviceProvider.GetService<ICustomerService>();

            if (_customerService == null)
            {
                throw new ArgumentNullException(nameof(_customerService));
            }
        }

        [HttpGet("list-customers")]
        [ProducesResponseType(typeof(List<CustomerDetailsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListCustomersAsync()
        {
            var listOrdersInActive = await _customerService.ListAsync().ConfigureAwait(false);

            return Ok(listOrdersInActive);
        }

        [HttpPost("register-customer")]
        [ProducesResponseType(typeof(CustomerDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterCustomerAsync([FromBody] CustomerCreateModel customerCreate)
        {
            var idCustomerRegistered = await _customerService.CreateAsync(customerCreate).ConfigureAwait(false);

            var customerRegistered = await _customerService.GetAsync(idCustomerRegistered).ConfigureAwait(false);

            if (customerRegistered == null) return BadRequest();

            return Ok(customerRegistered);
        }

        [HttpGet("details-customer/{customerNumber}")]
        [ProducesResponseType(typeof(CustomerDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DetailsAsync(int customerNumber)
        {
            try
            {
                var customerDetails = await _customerService.GetAsync(customerNumber).ConfigureAwait(false);

                if (customerDetails == null)
                {
                    return NotFound();
                }

                return Ok(customerDetails);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("edit-customer/{customerNumber}")]
        [ProducesResponseType(typeof(CustomerDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int customerNumber, [FromBody] CustomerUpdateModel updateModel)
        {
            if (updateModel.Id != customerNumber)
            {
                return BadRequest();
            }
            try
            {
                await _customerService.UpdateAsync(updateModel).ConfigureAwait(false);
                var customerDetails = await _customerService.GetAsync(customerNumber).ConfigureAwait(false);

                return Ok(customerDetails);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
