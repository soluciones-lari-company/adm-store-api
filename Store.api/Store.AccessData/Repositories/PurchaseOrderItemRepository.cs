using Microsoft.EntityFrameworkCore;
using Store.AccessData.Interfaces;
using Store.Models.Models.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.AccessData.Repositories
{
    internal class PurchaseOrderItemRepository : IPurchaseOrderItemRepository
    {
        private readonly StoreDC _storeCtx;


        public PurchaseOrderItemRepository(StoreDC storeCtx)
        {
            _storeCtx = storeCtx;
        }

        public async Task<PurchaseOrderItemDetailsModel> DetailsAsync(string itemCode)
        {
            var qr_detailsItem = from items in _storeCtx.PurchaseOrderItems
                                 where items.ItemCode == itemCode
                                 select new PurchaseOrderItemDetailsModel
                                 {
                                     DescriptionItem = items.DescriptionItem,
                                     Comments = items.Comments,
                                     CreatedAt = items.CreatedAt,
                                     CreatedBy = items.CreatedBy,
                                     ItemCode = items.ItemCode,
                                     LineNum = items.LineNum,
                                     Quantity = items.Quantity,
                                     Reference1 = items.Reference1,
                                     Total = items.Total,
                                     PriceByGrs = items.PriceByGrs,
                                     WeightItem = items.WeightItem,
                                     FactorRevenue = items.FactorRevenue,
                                     UnitPrice = items.UnitPrice,
                                     UpdatedAt = items.UpdatedAt,
                                     PublicPrice = items.PublicPrice,
                                     IsSold = items.IsSold,
                                 };

            return await qr_detailsItem.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public void MoveToStolenAsync(string itemCode, bool isStolen)
        {
            var ItemDetails = _storeCtx.PurchaseOrderItems.FirstOrDefault(item => item.ItemCode == itemCode);

            if (ItemDetails == null)
            {
                throw new NullReferenceException(nameof(ItemDetails));
            }

            ItemDetails.IsSold = isStolen;
            //ItemDetails.CreatedAt = DateTime.Now;
            ItemDetails.UpdatedAt = DateTime.Now;

            _storeCtx.SaveChanges();
        }
    }
}
