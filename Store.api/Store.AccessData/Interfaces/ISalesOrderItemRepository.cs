using Store.Models.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Interfaces
{
    internal interface ISalesOrderItemRepository
    {
        public Task<int> CreateAsync(SalesOrderItemCreateModel itemCreateModel);
        public Task<SalesOrderItemDetailsModel> DetailsAsync(int docNum, string itemCode);
        public Task DeleteAsync(int docNum, string itemCode);
        public Task<bool> ExistsInOrderAsync(int docNum, string itemCode);
        public Task<List<SalesOrderItemDetailsModel>> ListAsync(int docNum);
        public Task<decimal> TotalItemsAsync(int docNum);
        public Task UpdateAsync(SalesOrderItemUpdateModel itemUpdateModel);
    }
}
