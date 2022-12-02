using Store.Models.Models.PurchaseOrder;
using System.Threading.Tasks;

namespace Store.AccessData.Interfaces
{
    internal interface IPurchaseOrderItemRepository
    {
        public void MoveToStolenAsync(string itemCode, bool isStolen);
        public Task<PurchaseOrderItemDetailsModel?> DetailsAsync(string itemCode);
    }
}
