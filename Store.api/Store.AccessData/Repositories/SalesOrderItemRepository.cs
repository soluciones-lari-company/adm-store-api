using Microsoft.EntityFrameworkCore;
using Store.AccessData.Entities;
using Store.AccessData.Interfaces;
using Store.Models.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Repositories
{
    internal class SalesOrderItemRepository: ISalesOrderItemRepository
    {
        private readonly StoreDC _storeCtx;

        public SalesOrderItemRepository(StoreDC storeCtx)
        {
            _storeCtx = storeCtx;
        }
        public async Task<int> CreateAsync(SalesOrderItemCreateModel itemCreateModel)
        {
            var newItem = new SalesOrderItem
            {
                DocNum = itemCreateModel.DocNum,
                Comments = itemCreateModel.Comments,
                ItemCode = itemCreateModel.ItemCode,
                UnitPrice = itemCreateModel.UnitPrice,
                LineNum = 0,
                Quantity = itemCreateModel.Quantity,
                Reference1 = itemCreateModel.Reference1,
                Reference2 = itemCreateModel.Reference2,
                Total = itemCreateModel.Total,
                // TODO service-user
                CreatedBy = "USER-SYS",
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            };

            await _storeCtx.SalesOrderItems.AddAsync(newItem).ConfigureAwait(false);
            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);

            return newItem.Id;
        }

        public async Task DeleteAsync(int docNum, string itemCode)
        {
            var lineRegistered = await GetDetails(docNum, itemCode).ConfigureAwait(false);

            _storeCtx.SalesOrderItems.Remove(lineRegistered);
            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<SalesOrderItemDetailsModel?> DetailsAsync(int docNum, string itemCode)
        {
            var qr_salesLine = from item in _storeCtx.SalesOrderItems
                               where item.DocNum == docNum && item.ItemCode == itemCode
                               select new SalesOrderItemDetailsModel
                               {
                                   Id = item.Id,
                                   DocNum = item.DocNum,
                                   ItemCode = item.ItemCode,
                                   UnitPrice = item.UnitPrice,
                                   Quantity = item.Quantity,
                                   Total = item.Total,
                                   LineNum = item.LineNum,
                                   Reference1 = item.Reference1,
                                   Reference2 = item.Reference2,
                                   Comments = item.Comments,
                                   CreatedBy = item.CreatedBy,
                                   CreatedAt = item.CreatedAt,
                                   UpdatedAt = item.UpdatedAt
                               };
            return await qr_salesLine.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<bool> ExistsInOrderAsync(int docNum, string itemCode)
        {
            return await _storeCtx.SalesOrderItems.AnyAsync(item => item.DocNum == docNum && item.ItemCode == itemCode).ConfigureAwait(false);
        }

        public async Task<List<SalesOrderItemDetailsModel>> ListAsync(int docNum)
        {
            var qr_salesLines = from item in _storeCtx.SalesOrderItems
                                where item.DocNum == docNum
                                select new SalesOrderItemDetailsModel
                                {
                                    Id = item.Id,
                                    DocNum = item.DocNum,
                                    ItemCode = item.ItemCode,
                                    UnitPrice = item.UnitPrice,
                                    Quantity = item.Quantity,
                                    Total = item.Total,
                                    LineNum = item.LineNum,
                                    Reference1 = item.Reference1,
                                    Reference2 = item.Reference2,
                                    Comments = item.Comments,
                                    CreatedBy = item.CreatedBy,
                                    CreatedAt = item.CreatedAt,
                                    UpdatedAt = item.UpdatedAt
                                };
            return await qr_salesLines.ToListAsync().ConfigureAwait(false);
        }

        public async Task<decimal> TotalItemsAsync(int docNum)
        {
            return await _storeCtx.SalesOrderItems.Where(item => item.DocNum == docNum).SumAsync(item => item.Total).ConfigureAwait(false);
        }

        public async Task UpdateAsync(SalesOrderItemUpdateModel itemUpdateModel)
        {
            var lineRegistered = await GetDetails(itemUpdateModel.DocNum, itemUpdateModel.ItemCode).ConfigureAwait(false);
            lineRegistered.Id = itemUpdateModel.Id;
            lineRegistered.DocNum = itemUpdateModel.DocNum;
            lineRegistered.ItemCode = itemUpdateModel.ItemCode;
            lineRegistered.UnitPrice = itemUpdateModel.UnitPrice;
            lineRegistered.Quantity = itemUpdateModel.Quantity;
            lineRegistered.Total = itemUpdateModel.Total;
            lineRegistered.LineNum = itemUpdateModel.LineNum;
            lineRegistered.Reference1 = itemUpdateModel.Reference1;
            lineRegistered.Reference2 = itemUpdateModel.Reference2;
            lineRegistered.Comments = itemUpdateModel.Comments;
            lineRegistered.UpdatedAt = DateTime.Now;

            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<SalesOrderItem> GetDetails(int docNum, string itemCode)
        {
            var lineRegistered = await _storeCtx.SalesOrderItems.FirstOrDefaultAsync(line => line.ItemCode == itemCode && line.DocNum == docNum).ConfigureAwait(false);

            if (lineRegistered == null)
            {
                throw new NullReferenceException(nameof(lineRegistered));
            }

            return lineRegistered;
        }
    }
}
