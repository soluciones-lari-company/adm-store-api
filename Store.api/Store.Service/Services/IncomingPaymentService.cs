using Microsoft.Extensions.Logging;
using Store.AccessData.Interfaces;
using Store.Models.Models.IncomingPayment;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Services
{
    internal class IncomingPaymentService : IIncomingPaymentService
    {
        private readonly IBussinesAccountRepository _accountRepository;
        private readonly IIncomingPaymentRepository _incomingPaymentRepository;
        private readonly IBussinesAccountRepository _bussinesAccountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<IncomingPaymentService> _logger;

        public IncomingPaymentService(
            IBussinesAccountRepository accountRepository, 
            IIncomingPaymentRepository incomingPaymentRepository, 
            IBussinesAccountRepository bussinesAccountRepository, 
            ICustomerRepository customerRepository, 
            ILogger<IncomingPaymentService> logger)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _incomingPaymentRepository = incomingPaymentRepository ?? throw new ArgumentNullException(nameof(incomingPaymentRepository));
            _bussinesAccountRepository = bussinesAccountRepository ?? throw new ArgumentNullException(nameof(bussinesAccountRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CancelAsync(int idIncomingPayment)
        {
            var detailsPayment  = await _incomingPaymentRepository.GetAsync(idIncomingPayment);

            if(detailsPayment == null)
            {
                throw new NullReferenceException(nameof(detailsPayment));   
            }
            _incomingPaymentRepository.Canceled = true;
            _incomingPaymentRepository.CanceledDate = DateTime.Now;
            // TODO service-user
            _incomingPaymentRepository.CanceledBy = "USER-SYS";
            _incomingPaymentRepository.UpdatedAt = DateTime.Now;

            await _incomingPaymentRepository.SaveAsync(AccessData.Enums.SaveAction.Update).ConfigureAwait(false);
        }

        public async Task<int> CreateAsync(IncomingPaymentCreateModel incomingPaymentCreate)
        {
            if(incomingPaymentCreate == null)
            {
                throw new ArgumentNullException(nameof(incomingPaymentCreate));
            }

            var customer = await _customerRepository.DetailsAsync(incomingPaymentCreate.Customer).ConfigureAwait(false);

            if(customer == null)
            {
                throw new NullReferenceException(nameof(customer));
            }


            var accountDefault = await _accountRepository.GetDefaultAsync().ConfigureAwait(false);
            if (accountDefault == null)
            {
                throw new NullReferenceException("No existe una cuenta default para pagos recibidos");
            }

            _incomingPaymentRepository.Customer = incomingPaymentCreate.Customer;
            _incomingPaymentRepository.DocNum = incomingPaymentCreate.DocNum;
            _incomingPaymentRepository.Total = incomingPaymentCreate.Total;
            _incomingPaymentRepository.PaymentDate = incomingPaymentCreate.PaymentDate;
            _incomingPaymentRepository.BussinesAccount = accountDefault.Id;
            _incomingPaymentRepository.Canceled = false;
            _incomingPaymentRepository.CanceledDate = null;
            _incomingPaymentRepository.CanceledBy = "";
            _incomingPaymentRepository.Comments = incomingPaymentCreate.Comments;
            // TODO service-user
            _incomingPaymentRepository.CreatedBy = "USER-SYS";
            _incomingPaymentRepository.CreatedAt = DateTime.Now;
            _incomingPaymentRepository.UpdatedAt = DateTime.Now;

            await _incomingPaymentRepository.SaveAsync(AccessData.Enums.SaveAction.Create).ConfigureAwait(false);

            _accountRepository.AddHistoryLine(incomingPaymentCreate.Total, BussinesAccountHistoryType.entrada, BussinesAccountDocRefType.incommingPayment, _incomingPaymentRepository.Id, $"Abono [{customer.FullName}]");
            _accountRepository.Balance = _accountRepository.History.Sum(line => line.Total);
            await _accountRepository.SaveAsync(AccessData.Enums.SaveAction.Update);

            return _incomingPaymentRepository.Id;
        }

        public async Task<IncomingPaymentDetailsModel> GetAsync(int idIncomingPayment)
        {
            var detailsPayment = await _incomingPaymentRepository.GetAsync(idIncomingPayment);

            if (detailsPayment == null)
            {
                throw new NullReferenceException(nameof(detailsPayment));
            }

            return detailsPayment;
        }

        public async Task<List<IncomingPaymentDetailsModel>> ListAsync(int idCustomer)
        {
            var listPayments = await _incomingPaymentRepository.ListAsync(idCustomer).ConfigureAwait(false);

            return listPayments;
        }
    }
}
