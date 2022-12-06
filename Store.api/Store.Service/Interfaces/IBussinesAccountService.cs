using Store.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Interfaces
{
    internal interface IBussinesAccountService
    {
        public Task<int> CreateAsync(BussinesAccountCreateModel bussinesAccountCreate);
        public Task<BussinesAccountDetailsModel> DetailsAsync(int idBussinesAccount);
        public Task<List<BussinesAccountDetailsModel>> ListAsync();
        public Task<List<BussinesAccountHistoryDetailsModel>> GetHistory(int idBussinesAccount);
        public Task UpdateAsync(BussinesAccountUpdateModel bussinesAccountUpdate);
    }
}
