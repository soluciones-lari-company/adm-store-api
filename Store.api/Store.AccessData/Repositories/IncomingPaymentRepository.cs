using Microsoft.EntityFrameworkCore;
using Store.AccessData.Entities;
using Store.AccessData.Enums;
using Store.AccessData.Interfaces;
using Store.Models.Models.Customer;
using Store.Models.Models.IncomingPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.AccessData.Repositories
{
    public class IncomingPaymentRepository : IIncomingPaymentRepository
    {
        #region Propierties
        private readonly StoreDC _storeCtx;
        public int Id 
        {
            get { return _incomingPayment.Id; }
        }
        public int Customer 
        { 
            get { return _incomingPayment.Customer; } 
            set { _incomingPayment.Customer = value; } 
        }
        public int? DocNum
        {
            get { return _incomingPayment.DocNum; }
            set { _incomingPayment.DocNum = value; }
        }
        public decimal Total
        {
            get { return _incomingPayment.Total; }
            set { _incomingPayment.Total = value; }
        }
        public DateTime PaymentDate
        {
            get { return _incomingPayment.PaymentDate; }
            set { _incomingPayment.PaymentDate = value; }
        }
        public int BussinesAccount
        {
            get { return _incomingPayment.BussinesAccount; }
            set { _incomingPayment.BussinesAccount = value; }
        }
        public bool? Canceled
        {
            get { return _incomingPayment.Canceled; }
            set { _incomingPayment.Canceled = value; }
        }
        public DateTime? CanceledDate
        {
            get { return _incomingPayment.CanceledDate; }
            set { _incomingPayment.CanceledDate = value; }
        }
        public string CanceledBy
        {
            get { return _incomingPayment.CanceledBy; }
            set { _incomingPayment.CanceledBy = value; }
        }
        public string Comments
        {
            get { return _incomingPayment.Comments; }
            set { _incomingPayment.Comments = value; }
        }
        public string CreatedBy
        {
            get { return _incomingPayment.CreatedBy; }
            set { _incomingPayment.CreatedBy = value; }
        }
        public DateTime CreatedAt
        {
            get { return _incomingPayment.CreatedAt; }
            set { _incomingPayment.CreatedAt = value; }
        }
        public DateTime UpdatedAt
        {
            get { return _incomingPayment.UpdatedAt; }
            set { _incomingPayment.UpdatedAt = value; }
        }
        private StatusProccess _statusProccess = StatusProccess.New;
        private IncommingPayment _incomingPayment;
        #endregion

        public IncomingPaymentRepository(StoreDC storeCtx)
        {
            _storeCtx = storeCtx;
            _incomingPayment = new IncommingPayment();
        }

        public Task DeleteAsync()
        {
            ValidBeforeAction();
            _storeCtx.IncommingPayments.Remove(_incomingPayment);

            return Task.CompletedTask;
        }

        public async Task<IncomingPaymentDetailsModel> GetAsync(int idIncomingPayment)
        {
            return await PopulateData(payment => payment.Id == idIncomingPayment).ConfigureAwait(false);
        }

        public async Task<IncomingPaymentDetailsModel> GetAsync(int idIncomingPayment, int idSalesOrder)
        {
            return await PopulateData(payment => payment.Id == idIncomingPayment && Customer == idSalesOrder).ConfigureAwait(false);
        }

        public async Task<List<IncomingPaymentDetailsModel>> ListAsync(int idCustomer)
        {
            #region query
            var qr_payment = from payment in _storeCtx.IncommingPayments
                             where payment.Customer == idCustomer
                             select new IncomingPaymentDetailsModel
                             {
                                 Id = payment.Id,
                                 Customer = new CustomerDetailsModel
                                 {
                                     Id = payment.CustomerNavigation.Id,
                                     FullName = payment.CustomerNavigation.FullName,
                                     Email = payment.CustomerNavigation.Email,
                                     PhoneNumber = payment.CustomerNavigation.PhoneNumber,
                                     Group1 = payment.CustomerNavigation.Group1,
                                     Group2 = payment.CustomerNavigation.Group2,
                                     Group3 = payment.CustomerNavigation.Group3,
                                     CreatedBy = payment.CustomerNavigation.CreatedBy,
                                     UpdatedAt = payment.CustomerNavigation.UpdatedAt,
                                     CreatedAt = payment.CustomerNavigation.UpdatedAt,
                                 },
                                 DocNum = payment.DocNum,
                                 Total = payment.Total,
                                 PaymentDate = payment.PaymentDate,
                                 BussinesAccount = new Models.Models.BussinesAccountDetailsModel
                                 {
                                     Id = payment.BussinesAccountNavigation.Id,
                                     AccountName = payment.BussinesAccountNavigation.AccountName,
                                     Balance = payment.BussinesAccountNavigation.Balance,
                                     Comments = payment.BussinesAccountNavigation.Comments,
                                     CreatedBy = payment.BussinesAccountNavigation.CreatedBy,
                                     CreatedAt = payment.BussinesAccountNavigation.CreatedAt,
                                     UpdatedAt = payment.BussinesAccountNavigation.UpdatedAt,
                                 },
                                 Canceled = payment.Canceled,
                                 CanceledDate = payment.CanceledDate,
                                 CanceledBy = payment.CanceledBy,
                                 Comments = payment.Comments,
                                 CreatedBy = payment.CreatedBy,
                                 CreatedAt = payment.CreatedAt,
                                 UpdatedAt = payment.UpdatedAt,
                             };
            #endregion

            return await qr_payment.ToListAsync().ConfigureAwait(false);
        }

        public async Task SaveAsync(SaveAction action)
        {
            ValidBeforeAction();
            if(action == SaveAction.Create)
            {
                _incomingPayment.Id = 0;
                await _storeCtx.IncommingPayments.AddAsync(_incomingPayment).ConfigureAwait(false);
            }

            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        private void ValidBeforeAction()
        {
            string messageProcess = string.Empty;
            switch (_statusProccess)
            {
                case StatusProccess.New:
                    messageProcess = "Please select an incoming payment";
                    break;
                case StatusProccess.Complete:
                    messageProcess = string.Empty;
                    break;
                case StatusProccess.Failed:
                    messageProcess = "An error ocurred during the process";
                    break;
                case StatusProccess.NotFound:
                    messageProcess = "The incoming payment selected  was not found";
                    break;
                default:
                    messageProcess = string.Empty;
                    break;
            }

            if (messageProcess != string.Empty)
            {
                throw new NullReferenceException(messageProcess);
            }
        }

        private async Task<IncomingPaymentDetailsModel> PopulateData(System.Linq.Expressions.Expression<Func<IncommingPayment, bool>> expressions)
        {
            var paymentRegistered = await _storeCtx.IncommingPayments.FirstOrDefaultAsync(expressions).ConfigureAwait(false);
            if (paymentRegistered == null)
            {
                _incomingPayment = new IncommingPayment();
                _statusProccess = StatusProccess.NotFound;
                return null;
            }

            _incomingPayment = paymentRegistered;

            return new IncomingPaymentDetailsModel
            {
                Id = paymentRegistered.Id,
                Customer = new CustomerDetailsModel
                {
                    Id = paymentRegistered.CustomerNavigation.Id,
                    FullName = paymentRegistered.CustomerNavigation.FullName,
                    Email = paymentRegistered.CustomerNavigation.Email,
                    PhoneNumber = paymentRegistered.CustomerNavigation.PhoneNumber,
                    Group1 = paymentRegistered.CustomerNavigation.Group1,
                    Group2 = paymentRegistered.CustomerNavigation.Group2,
                    Group3 = paymentRegistered.CustomerNavigation.Group3,
                    CreatedBy = paymentRegistered.CustomerNavigation.CreatedBy,
                    UpdatedAt = paymentRegistered.CustomerNavigation.UpdatedAt,
                    CreatedAt = paymentRegistered.CustomerNavigation.UpdatedAt,
                },
                DocNum = paymentRegistered.DocNum,
                Total = paymentRegistered.Total,
                PaymentDate = paymentRegistered.PaymentDate,
                BussinesAccount = new Models.Models.BussinesAccountDetailsModel
                {
                    Id = paymentRegistered.BussinesAccountNavigation.Id,
                    AccountName = paymentRegistered.BussinesAccountNavigation.AccountName,
                    Balance = paymentRegistered.BussinesAccountNavigation.Balance,
                    Comments = paymentRegistered.BussinesAccountNavigation.Comments,
                    CreatedBy = paymentRegistered.BussinesAccountNavigation.CreatedBy,
                    CreatedAt = paymentRegistered.BussinesAccountNavigation.CreatedAt,
                    UpdatedAt = paymentRegistered.BussinesAccountNavigation.UpdatedAt,
                },
                Canceled = paymentRegistered.Canceled,
                CanceledDate = paymentRegistered.CanceledDate,
                CanceledBy = paymentRegistered.CanceledBy,
                Comments = paymentRegistered.Comments,
                CreatedBy = paymentRegistered.CreatedBy,
                CreatedAt = paymentRegistered.CreatedAt,
                UpdatedAt = paymentRegistered.UpdatedAt,
            };
        }
    }
}
