using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Store.Models.Models;
using Store.Models.Models.PurchaseOrder;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IPurchaseOrderService _purchaseOrderService;

        public ItemController(ILogger<ItemController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _purchaseOrderService = serviceProvider.GetService<IPurchaseOrderService>();

            if (_purchaseOrderService == null)
            {
                throw new ArgumentNullException(nameof(_purchaseOrderService));
            }
        }

        [HttpGet("item-details/{itemCode}")]
        [ProducesResponseType(typeof(PurchaseOrderItemDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DetailsItemAsync(string itemCode)
        {
            if (string.IsNullOrWhiteSpace(itemCode))
            {
                return BadRequest();
            }
            try
            {
                var detailsLineRegistered = await _purchaseOrderService.DetailsItemAsync(itemCode).ConfigureAwait(false);

                if (detailsLineRegistered == null) return NotFound();

                return Ok(detailsLineRegistered);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
