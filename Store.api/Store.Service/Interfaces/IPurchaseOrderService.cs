using Store.Models.Models.PurchaseOrder;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Interfaces
{
    internal interface IPurchaseOrderService
    {
        public Task<PurchaseOrderItemDetailsModel> DetailsItemAsync(string itemCode);
    }
}
