using Store.AccessData.Enums;
using Store.Models.Models.IncomingPayment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Interfaces
{
    internal interface IIncomingPaymentRepository
    {
        public int Id { get; }
        public int Customer { get; set; }
        public int? DocNum { get; set; }
        public decimal Total { get; set; }
        public DateTime PaymentDate { get; set; }
        public int BussinesAccount { get; set; }
        public bool? Canceled { get; set; }
        public DateTime? CanceledDate { get; set; }
        public string CanceledBy { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// delete the incoming payment selected
        /// </summary>
        /// <returns></returns>
        public Task DeleteAsync();
        /// <summary>
        /// select an incoming payment
        /// </summary>
        /// <param name="idIncomingPayment"></param>
        /// <returns></returns>
        public Task<IncomingPaymentDetailsModel> GetAsync(int idIncomingPayment);
        /// <summary>
        /// select a incoming payment by id and sales order
        /// </summary>
        /// <param name="idIncomingPayment">id</param>
        /// <param name="idSalesOrder">id sales order</param>
        /// <returns></returns>
        public Task<IncomingPaymentDetailsModel> GetAsync(int idIncomingPayment, int idSalesOrder);
        /// <summary>
        /// list incoming payments
        /// </summary>
        /// <param name="idCustomer">id customer</param>
        /// <returns></returns>
        public Task<List<IncomingPaymentDetailsModel>> ListAsync(int idCustomer);
        /// <summary>
        /// save changes
        /// </summary>
        /// <returns></returns>
        public Task SaveAsync(SaveAction action);
    }
}
