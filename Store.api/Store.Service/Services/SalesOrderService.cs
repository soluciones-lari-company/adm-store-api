using Microsoft.Extensions.Logging;
using Store.AccessData.Interfaces;
using Store.Models.Models.Customer;
using Store.Models.Models.SalesOrder;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Services
{
    internal class SalesOrderService: ISalesOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<SalesOrderService> _logger;
        private readonly ISalesOrderItemRepository _salesOrderItemRepository;
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IPurchaseOrderItemRepository _purchaseOrderRepository;
        private readonly IBussinesAccountRepository _accountRepository;
        private readonly IIncomingPaymentRepository _incomingPaymentRepository;

        public SalesOrderService(ICustomerRepository customerRepository,
            ILogger<SalesOrderService> logger,
            ISalesOrderItemRepository salesOrderItemRepository,
            ISalesOrderRepository salesOrderRepository,
            IPurchaseOrderItemRepository purchaseOrderRepository,
            IBussinesAccountRepository bussinesAccountRepository,
            IIncomingPaymentRepository incomingPaymentRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _salesOrderItemRepository = salesOrderItemRepository ?? throw new ArgumentNullException(nameof(salesOrderItemRepository));
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _purchaseOrderRepository = purchaseOrderRepository ?? throw new ArgumentNullException(nameof(purchaseOrderRepository));
            _accountRepository = bussinesAccountRepository ?? throw new ArgumentNullException(nameof(bussinesAccountRepository));
            _incomingPaymentRepository = incomingPaymentRepository;
        }

        public async Task<int> CreateAsync(SalesOrderCreateModel salesOrderCreate)
        {
            if (salesOrderCreate == null) throw new ArgumentNullException(nameof(salesOrderCreate));

            var detailsCustomer = await GetCustomer(salesOrderCreate.Customer).ConfigureAwait(false);

            var salesOrderCreated = await _salesOrderRepository.CreateAsync(salesOrderCreate.Customer, salesOrderCreate.DocDate, salesOrderCreate.DocStatus, salesOrderCreate.PaymentMethod).ConfigureAwait(false);

            return salesOrderCreated;
        }

        private async Task<CustomerDetailsModel> GetCustomer(int customerNumber)
        {
            var detailsCustomer = await _customerRepository.DetailsAsync(customerNumber).ConfigureAwait(false);

            if (detailsCustomer == null)
            {
                throw new NullReferenceException(nameof(detailsCustomer));
            }

            return detailsCustomer;
        }

        public async Task AddLine(SalesOrderItemCreateModel itemCreate)
        {
            if (itemCreate == null) throw new ArgumentNullException(nameof(itemCreate));

            var orderRegistered = await _salesOrderRepository.DetailsAsync(itemCreate.DocNum).ConfigureAwait(false);

            if (orderRegistered == null) throw new NullReferenceException(nameof(orderRegistered));

            await _salesOrderItemRepository.CreateAsync(itemCreate).ConfigureAwait(false);
            await _salesOrderRepository.UpdateTotalAsync(itemCreate.DocNum).ConfigureAwait(false);
            _purchaseOrderRepository.MoveToStolenAsync(itemCreate.ItemCode, true);
        }

        public async Task CancelAsync(int docNum)
        {
            var orderRegistered = await _salesOrderRepository.DetailsAsync(docNum).ConfigureAwait(false);

            if (orderRegistered == null) throw new NullReferenceException(nameof(orderRegistered));

            var lines = await _salesOrderItemRepository.ListAsync(docNum).ConfigureAwait(false);

            lines.ForEach(line =>
            {
                _purchaseOrderRepository.MoveToStolenAsync(line.ItemCode, false);
            });

            await _salesOrderRepository.UpdateAsync(new SalesOrderUpdateModel
            {
                DocNum = orderRegistered.DocNum,
                Customer = orderRegistered.Customer.Id,
                DocDate = orderRegistered.DocDate,
                DocTotal = orderRegistered.DocTotal,
                DocStatus = "C",
                Canceled = true,
                CandeledDate = DateTime.Now,
                // TODO: USER-SYS
                CanceledBy = "USER-SYS",
                MethodPayment = orderRegistered.MethodPayment,
            });

            if(orderRegistered.MethodPayment == "PUE")
            {
                var payment = await _incomingPaymentRepository.GetBySalesOrderAsync(docNum).ConfigureAwait(false);
                if(payment != null)
                {
                    _incomingPaymentRepository.Canceled = true;
                    _incomingPaymentRepository.CanceledBy = "USER-SYS";
                    _incomingPaymentRepository.CanceledDate = DateTime.Now;

                    await _incomingPaymentRepository.SaveAsync(AccessData.Enums.SaveAction.Update).ConfigureAwait(false);
                }
            }


        }

        public async Task CompleteAsync(int docNum, string paymentMethod)
        {
            //validate exists order
            var orderRegistered = await _salesOrderRepository.DetailsAsync(docNum).ConfigureAwait(false);

            if (orderRegistered == null)
            {
                throw new NullReferenceException("Orden no encontrada");
            }
            //validate status
            if(orderRegistered.DocStatus != "A")
            {
                throw new InvalidOperationException("Error, esta orden no puede ser procesada");
            }
            //validate default account for icoming payments
            int accountDefaultId  = 0;
            if (paymentMethod == "PUE")
            {
                var accountDefault = await _accountRepository.GetDefaultAsync().ConfigureAwait(false);
                if (accountDefault == null)
                {
                    throw new NullReferenceException("No existe una cuenta default para pagos recibidos");
                }

                accountDefaultId = accountDefault.Id;
            }

            await _salesOrderRepository.ChangeStatusAsync(docNum, "F").ConfigureAwait(false);
            await _salesOrderRepository.ChangePaymentMethodAsync(docNum, paymentMethod).ConfigureAwait(false);
            
            if (paymentMethod == "PUE")
            {
                _incomingPaymentRepository.Customer = orderRegistered.Customer.Id;
                _incomingPaymentRepository.DocNum = orderRegistered.DocNum;
                _incomingPaymentRepository.Total = orderRegistered.DocTotal;
                _incomingPaymentRepository.PaymentDate = orderRegistered.DocDate;
                _incomingPaymentRepository.BussinesAccount = accountDefaultId;
                _incomingPaymentRepository.Canceled = false;
                _incomingPaymentRepository.CanceledDate = null;
                _incomingPaymentRepository.CanceledBy = "";
                _incomingPaymentRepository.Comments = "Pago de contado";
                // TODO service-user
                _incomingPaymentRepository.CreatedBy = "USER-SYS";
                _incomingPaymentRepository.CreatedAt = DateTime.Now;
                _incomingPaymentRepository.UpdatedAt = DateTime.Now;

                await _incomingPaymentRepository.SaveAsync(AccessData.Enums.SaveAction.Create).ConfigureAwait(false);

                _accountRepository.AddHistoryLine(orderRegistered.DocTotal, BussinesAccountHistoryType.entrada, BussinesAccountDocRefType.incommingPayment, _incomingPaymentRepository.Id, $"Pago [{orderRegistered.Customer.FullName}]");
                _accountRepository.Balance += orderRegistered.DocTotal;
                await _accountRepository.SaveAsync(AccessData.Enums.SaveAction.Update);
            }
        }

        public async Task DeleteLine(int docNum, string itemCode)
        {
            await DetailsLine(docNum, itemCode).ConfigureAwait(false);

            await _salesOrderItemRepository.DeleteAsync(docNum, itemCode).ConfigureAwait(false);
            _purchaseOrderRepository.MoveToStolenAsync(itemCode, false);
        }

        public async Task<SalesOrderDetailsFullModel> GetAsync(int docNum)
        {
            var orderHeader = await _salesOrderRepository.DetailsAsync(docNum).ConfigureAwait(false);

            if (orderHeader == null) throw new NullReferenceException(nameof(orderHeader));

            var orderLines = await _salesOrderItemRepository.ListAsync(docNum).ConfigureAwait(false);

            return new SalesOrderDetailsFullModel
            {
                Header = orderHeader,
                Lines = orderLines
            };
        }

        public async Task<List<SalesOrderDetailsModel>> ListAsync()
        {
            var listOrders = await _salesOrderRepository.List().ConfigureAwait(false);

            return listOrders;
        }

        public async Task<List<SalesOrderDetailsModel>> ListAsync(int customerNumber)
        {
            var listOrdersByCustomer = await _salesOrderRepository.List(customerNumber).ConfigureAwait(false);

            return listOrdersByCustomer;
        }

        public async Task<List<SalesOrderDetailsModel>> ListAsync(string status)
        {
            var listOrdersByStatus = await _salesOrderRepository.List(status).ConfigureAwait(false);

            return listOrdersByStatus;
        }

        public async Task<List<SalesOrderItemDetailsModel>> ListLine(int docNum)
        {
            var listLinesByOrder = await _salesOrderItemRepository.ListAsync(docNum).ConfigureAwait(false);

            return listLinesByOrder;
        }

        public async Task UpdateAsync(SalesOrderUpdateModel salesOrderUpdate)
        {
            var orderRegistered = await _salesOrderRepository.DetailsAsync(salesOrderUpdate.DocNum).ConfigureAwait(false);

            if (orderRegistered == null) throw new NullReferenceException(nameof(orderRegistered));

            await _salesOrderRepository.UpdateAsync(salesOrderUpdate).ConfigureAwait(false);

        }

        public async Task UpdateLine(SalesOrderItemUpdateModel itemUpdate)
        {
            await DetailsLine(itemUpdate.DocNum, itemUpdate.ItemCode).ConfigureAwait(false);
            await _salesOrderItemRepository.UpdateAsync(itemUpdate).ConfigureAwait(false);
        }

        public async Task<SalesOrderItemDetailsModel> DetailsLine(int docNum, string itemCode)
        {
            var lineDetails = await _salesOrderItemRepository.DetailsAsync(docNum, itemCode).ConfigureAwait(false);

            if (lineDetails == null) throw new NullReferenceException(nameof(lineDetails));

            return lineDetails;
        }
    }
}
