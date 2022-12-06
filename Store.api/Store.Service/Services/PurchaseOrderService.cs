using Microsoft.Extensions.Logging;
using Store.AccessData.Interfaces;
using Store.Models.Models.PurchaseOrder;
using Store.Service.Interfaces;
using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Services
{
    internal class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly ILogger<PurchaseOrderService> _logger;
        private readonly IPurchaseOrderItemRepository _orderItemRepository;

        public PurchaseOrderService(IPurchaseOrderItemRepository orderItemRepository, ILogger<PurchaseOrderService> logger)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PurchaseOrderItemDetailsModel?> DetailsItemAsync(string itemCode)
        {
            if (string.IsNullOrWhiteSpace(itemCode)) throw new ArgumentNullException(itemCode);

            return await _orderItemRepository.DetailsAsync(itemCode).ConfigureAwait(false);
        }
    }
}
