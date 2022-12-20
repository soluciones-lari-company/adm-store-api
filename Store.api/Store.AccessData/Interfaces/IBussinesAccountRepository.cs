using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Models.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Interfaces
{
    internal interface IBussinesAccountRepository
    {
        public Task<int> CreateAsync(string name, string comments);
        public Task<BussinesAccountDetailsModel> DetailsAsync(int idBussinesAccount);
        public void SetAsDefault(int idBussinesAccount, bool isDefault);
        public Task<List<BussinesAccountDetailsModel>> GetAsDefault();
        public Task<List<BussinesAccountDetailsModel>> ListAsync();
        public Task<int> UpdateAsync(int idBussinesAccount, string name, string comments);
        public Task AddHistoryLine(int idBussinesAccount, decimal total, BussinesAccountHistoryType historyType, BussinesAccountDocRefType docRefType, int docRefNum, string comments);
        public Task<List<BussinesAccountHistoryDetailsModel>> GetHistory(int idBussinesAccoun);
    }

    internal enum BussinesAccountHistoryType
    {
        entrada = 1,
        salida = 2
    }

    internal enum BussinesAccountDocRefType
    {
        incommingPayment = 1,
        outCommingPayment = 2
    }
}
