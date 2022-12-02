using Store.Models.Models.SalesOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Interfaces
{
    internal interface ISalesOrderRepository
    {
        public Task<int> CreateAsync(int customerNumber, DateTime docDate, int docType, string docStatus, string methodPayment);
        public Task<SalesOrderDetailsModel> DetailsAsync(int docNum);
        public Task<List<SalesOrderDetailsModel>> List();
        public Task<List<SalesOrderDetailsModel>> List(string status);
        public Task<List<SalesOrderDetailsModel>> List(int customerNumber);
        public Task UpdateAsync(SalesOrderUpdateModel orderUpdateModel);
        public Task ChangeStatusAsync(int docNum, string status);
        public Task UpdateTotalAsync(int docNum);
    }
}
