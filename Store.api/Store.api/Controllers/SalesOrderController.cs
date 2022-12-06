using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Store.Models.Models;
using Store.Models.Models.Customer;
using Store.Models.Models.PurchaseOrder;
using Store.Models.Models.SalesOrder;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly ILogger<SalesOrderController> _logger;
        private readonly ISalesOrderService _salesOrderService;
        private readonly string[] validPaymentMethod = { "PUE", "PPD"};
        public SalesOrderController(ILogger<SalesOrderController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _salesOrderService = serviceProvider.GetService<ISalesOrderService>();

            if (_salesOrderService == null)
            {
                throw new ArgumentNullException(nameof(_salesOrderService));
            }
        }

        [HttpGet("list-orders-active")]
        [ProducesResponseType(typeof(List<SalesOrderDetailsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListOrdersInActiveAsync()
        {
            var listOrdersInActive = await _salesOrderService.ListAsync("A").ConfigureAwait(false);

            return Ok(listOrdersInActive);
        }

        [HttpGet("list-orders-by-customer/{customerNumber}")]
        [ProducesResponseType(typeof(List<SalesOrderDetailsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListOrdersByCustomerAsync(int customerNumber)
        {
            var listOrdersBycustomer = await _salesOrderService.ListAsync(customerNumber).ConfigureAwait(false);

            return Ok(listOrdersBycustomer);
        }

        [HttpGet("details-order/{docNum}")]
        [ProducesResponseType(typeof(SalesOrderDetailsFullModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DetailsOrderAsync(int docNum)
        {
            try
            {
                var orderDetails = await _salesOrderService.GetAsync(docNum).ConfigureAwait(false);

                if (orderDetails == null)
                {
                    return NotFound();
                }

                return Ok(orderDetails);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("register-order")]
        [ProducesResponseType(typeof(SalesOrderDetailsFullModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrderAsync([FromBody] SalesOrderCreateModel salesOrderCreate)
        {
            try
            {
                var idOrderRegistered = await _salesOrderService.CreateAsync(salesOrderCreate).ConfigureAwait(false);
                var orderDetails = await _salesOrderService.GetAsync(idOrderRegistered).ConfigureAwait(false);

                return Ok(orderDetails);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet("cancel-order/{docNum}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelOrderAsync(int docNum)
        {
            try
            {
                await _salesOrderService.CancelAsync(docNum).ConfigureAwait(false);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("complete-order/{docNum}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompleteOrderAsync(int docNum, string paymentMethod)
        {
            if (string.IsNullOrWhiteSpace(paymentMethod) || !validPaymentMethod.Contains(paymentMethod))
            {
                return BadRequest("Metodo de pago invalido");
            }

            try
            {
                await _salesOrderService.CompleteAsync(docNum, paymentMethod).ConfigureAwait(false);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("addNewline-order/{docNum}")]
        [ProducesResponseType(typeof(SalesOrderItemDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddLineAsync(int docNum, [FromBody] SalesOrderItemCreateModel newLine)
        {
            if (newLine.DocNum != docNum)
            {
                return BadRequest();
            }
            try
            {
                await _salesOrderService.AddLine(newLine).ConfigureAwait(false);
                var detailsLineRegistered = await _salesOrderService.DetailsLine(docNum, newLine.ItemCode).ConfigureAwait(false);

                return Ok(detailsLineRegistered);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet("detailsline-order/{docNum}/{itemCode}")]
        [ProducesResponseType(typeof(SalesOrderItemDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DetailsLineAsync(int docNum, string itemCode)
        {
            if (docNum == 0 || string.IsNullOrWhiteSpace(itemCode))
            {
                return BadRequest();
            }
            try
            {
                var detailsLineRegistered = await _salesOrderService.DetailsLine(docNum, itemCode).ConfigureAwait(false);

                return Ok(detailsLineRegistered);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPut("editline-order/{docNum}/{itemCode}")]
        [ProducesResponseType(typeof(SalesOrderItemDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditLineAsync(int docNum, string itemCode, [FromBody] SalesOrderItemUpdateModel lineUpdate)
        {
            if (docNum == 0 || string.IsNullOrWhiteSpace(itemCode) || lineUpdate.DocNum != docNum || lineUpdate.ItemCode != itemCode)
            {
                return BadRequest();
            }

            try
            {
                await _salesOrderService.UpdateLine(lineUpdate).ConfigureAwait(false);
                var detailsLineRegistered = await _salesOrderService.DetailsLine(docNum, itemCode).ConfigureAwait(false);
                return Ok(_salesOrderService);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpDelete("deleteline-order/{docNum}/{itemCode}")]
        [ProducesResponseType(typeof(SalesOrderItemDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLineAsync(int docNum, string itemCode)
        {
            if (docNum == 0 || string.IsNullOrWhiteSpace(itemCode))
            {
                return BadRequest();
            }

            try
            {
                await _salesOrderService.DeleteLine(docNum, itemCode).ConfigureAwait(false);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
