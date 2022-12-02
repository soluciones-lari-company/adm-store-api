using Store.Models.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Interfaces
{
    internal interface ICustomerRepository
    {
        public Task<int> CreateAsync(string fullName, string phoneNumber, string emailAddress);
        public Task<CustomerDetailsModel> ExistsAsync(string fullName);
        public Task<CustomerDetailsModel> DetailsAsync(int customerNumber);
        public Task<List<CustomerDetailsModel>> ListAsync();
        public Task DeleteAsync(int id);
        public Task UpdateAsync(CustomerUpdateModel customerUpdate);
    }
}
