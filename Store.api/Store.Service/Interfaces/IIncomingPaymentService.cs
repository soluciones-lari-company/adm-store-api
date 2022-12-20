using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Models.Models.IncomingPayment;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Interfaces
{
    internal interface IIncomingPaymentService
    {
        public Task<int> CreateAsync(IncomingPaymentCreateModel incomingPaymentCreate);
        public Task CancelAsync(int idIncomingPayment);
        public Task<IncomingPaymentDetailsModel> GetAsync(int idIncomingPayment);
        public Task<List<IncomingPaymentDetailsModel>> ListAsync(int idCustomer);
    }
}
