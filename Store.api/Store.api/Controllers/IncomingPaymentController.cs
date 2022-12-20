using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Store.Models.Models.IncomingPayment;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingPaymentController : ControllerBase
    {
        private readonly ILogger<IncomingPaymentController> _logger;
        private readonly IIncomingPaymentService _incomingPaymentService;

        public IncomingPaymentController(ILogger<IncomingPaymentController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _incomingPaymentService = serviceProvider.GetService<IIncomingPaymentService>();

            if (_incomingPaymentService == null)
            {
                throw new ArgumentNullException(nameof(_incomingPaymentService));
            }
        }

        [HttpGet("list-incoming-payments-customer/{idCustomer}")]
        [ProducesResponseType(typeof(List<IncomingPaymentDetailsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAccountsAsync(int idCustomer)
        {
            var listAccounts = await _incomingPaymentService.ListAsync(idCustomer).ConfigureAwait(false);

            return Ok(listAccounts);
        }

        [HttpGet("details-incoming-payment/{idIncomingPayment}")]
        [ProducesResponseType(typeof(IncomingPaymentDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DetailsPaymentAsync(int idIncomingPayment)
        {
            if(idIncomingPayment == 0)
            {
                return BadRequest();
            }
            try
            {
                var detailsPayment = await _incomingPaymentService.GetAsync(idIncomingPayment).ConfigureAwait(false);

                if (detailsPayment == null)
                {
                    return NotFound();
                }

                return Ok(detailsPayment);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("register-incoming-payment")]
        [ProducesResponseType(typeof(IncomingPaymentDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterPaymentAsync([FromBody] IncomingPaymentCreateModel paymentCreateModel)
        {
            if(paymentCreateModel == null)
            {
                return BadRequest();
            }
            
            try
            {
                var idIncomingPayment = await _incomingPaymentService.CreateAsync(paymentCreateModel).ConfigureAwait(false);
                var detailsPayment = await _incomingPaymentService.GetAsync(idIncomingPayment).ConfigureAwait(false);
                return Ok(detailsPayment);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPut("cancel-incoming-payment/{idIncomingPayment}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelPaymentAsync(int idIncomingPayment)
        {
            if (idIncomingPayment == 0)
            {
                return BadRequest();
            }

            try
            {
                 await _incomingPaymentService.CancelAsync(idIncomingPayment).ConfigureAwait(false);
                return Ok("incomin payment deleted");
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
