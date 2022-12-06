using Store.Models.Models.Customer;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Interfaces
{
    internal interface ICustomerService
    {
        public Task<CustomerDetailsModel> GetAsync(int customerNumber);
        public Task<CustomerDetailsModel> GetAsync(string fullName);
        public Task<List<CustomerDetailsModel>> ListAsync();
        public Task<int> CreateAsync(CustomerCreateModel customerCreate);
        public Task UpdateAsync(CustomerUpdateModel customerUpdate);
    }
}
