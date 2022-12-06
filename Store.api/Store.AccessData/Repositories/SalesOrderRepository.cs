using Microsoft.EntityFrameworkCore;
using Store.AccessData.Entities;
using Store.AccessData.Interfaces;
using Store.Models.Models.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Repositories
{
    internal class SalesOrderRepository: ISalesOrderRepository
    {
        private readonly StoreDC _storeCtx;

        public SalesOrderRepository(StoreDC storeCtx)
        {
            _storeCtx = storeCtx;
        }

        public async Task ChangePaymentMethodAsync(int docNum, string paymentMethod)
        {
            var orderRegistered = await GetAsync(docNum).ConfigureAwait(false);
            orderRegistered.MethodPayment = paymentMethod;
            orderRegistered.UpdatedAt = DateTime.Now;

            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task ChangeStatusAsync(int docNum, string status)
        {
            var orderRegistered = await GetAsync(docNum).ConfigureAwait(false);

            orderRegistered.DocStatus = status;
            orderRegistered.UpdatedAt = DateTime.Now;

            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> CreateAsync(int customerNumber, DateTime docDate, string docStatus, string methodPayment)
        {
            var newSalesOrder = new SalesOrder
            {
                DocDate = docDate,
                DocStatus = docStatus,
                DocTotal = 0,
                MethodPayment = methodPayment,
                Customer = customerNumber,
                Canceled = false,
                CanceledBy = string.Empty,
                CandeledDate = DateTime.Now,
                // TODO service-user
                CreatedBy = "USER-SYS",
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            };

            await _storeCtx.SalesOrders.AddAsync(newSalesOrder).ConfigureAwait(false);
            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);

            return newSalesOrder.DocNum;
        }

        public async Task<SalesOrderDetailsModel?> DetailsAsync(int docNum)
        {
            var qr_salesOrder = from salesOrder in _storeCtx.SalesOrders
                                where salesOrder.DocNum == docNum
                                select new SalesOrderDetailsModel
                                {
                                    DocNum = salesOrder.DocNum,
                                    DocDate = salesOrder.DocDate,
                                    DocStatus = salesOrder.DocStatus,
                                    DocTotal = salesOrder.DocTotal,
                                    MethodPayment = salesOrder.MethodPayment,
                                    Canceled = salesOrder.Canceled,
                                    CanceledBy = salesOrder.CanceledBy,
                                    CandeledDate = salesOrder.CandeledDate,
                                    CreatedBy = salesOrder.CreatedBy,
                                    UpdatedAt = salesOrder.UpdatedAt,
                                    CreatedAt = salesOrder.CreatedAt,
                                    Customer = new Models.Models.Customer.CustomerDetailsModel
                                    {
                                        Id = salesOrder.CustomerNavigation.Id,
                                        FullName = salesOrder.CustomerNavigation.FullName,
                                        Email = salesOrder.CustomerNavigation.Email,
                                        PhoneNumber = salesOrder.CustomerNavigation.PhoneNumber,
                                        Group1 = salesOrder.CustomerNavigation.Group1,
                                        Group2 = salesOrder.CustomerNavigation.Group2,
                                        Group3 = salesOrder.CustomerNavigation.Group3,
                                        CreatedBy = salesOrder.CustomerNavigation.CreatedBy,
                                        CreatedAt = salesOrder.CustomerNavigation.CreatedAt,
                                        UpdatedAt = salesOrder.CustomerNavigation.UpdatedAt
                                    }
                                };

            return await qr_salesOrder.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<List<SalesOrderDetailsModel>> List()
        {
            var qr_salesOrder = from salesOrder in _storeCtx.SalesOrders
                                select new SalesOrderDetailsModel
                                {
                                    DocNum = salesOrder.DocNum,
                                    DocDate = salesOrder.DocDate,
                                    DocStatus = salesOrder.DocStatus,
                                    DocTotal = salesOrder.DocTotal,
                                    MethodPayment = salesOrder.MethodPayment,
                                    Canceled = salesOrder.Canceled,
                                    CanceledBy = salesOrder.CanceledBy,
                                    CandeledDate = salesOrder.CandeledDate,
                                    CreatedBy = salesOrder.CreatedBy,
                                    UpdatedAt = salesOrder.UpdatedAt,
                                    CreatedAt = salesOrder.CreatedAt,
                                    Customer = new Models.Models.Customer.CustomerDetailsModel
                                    {
                                        Id = salesOrder.CustomerNavigation.Id,
                                        FullName = salesOrder.CustomerNavigation.FullName,
                                        Email = salesOrder.CustomerNavigation.Email,
                                        PhoneNumber = salesOrder.CustomerNavigation.PhoneNumber,
                                        Group1 = salesOrder.CustomerNavigation.Group1,
                                        Group2 = salesOrder.CustomerNavigation.Group2,
                                        Group3 = salesOrder.CustomerNavigation.Group3,
                                        CreatedBy = salesOrder.CustomerNavigation.CreatedBy,
                                        CreatedAt = salesOrder.CustomerNavigation.CreatedAt,
                                        UpdatedAt = salesOrder.CustomerNavigation.UpdatedAt
                                    }
                                };

            return await qr_salesOrder.ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<SalesOrderDetailsModel>> List(string status)
        {
            var qr_salesOrder = from salesOrder in _storeCtx.SalesOrders
                                where salesOrder.DocStatus == status
                                select new SalesOrderDetailsModel
                                {
                                    DocNum = salesOrder.DocNum,
                                    DocDate = salesOrder.DocDate,
                                    DocStatus = salesOrder.DocStatus,
                                    DocTotal = salesOrder.DocTotal,
                                    MethodPayment = salesOrder.MethodPayment,
                                    Canceled = salesOrder.Canceled,
                                    CanceledBy = salesOrder.CanceledBy,
                                    CandeledDate = salesOrder.CandeledDate,
                                    CreatedBy = salesOrder.CreatedBy,
                                    UpdatedAt = salesOrder.UpdatedAt,
                                    CreatedAt = salesOrder.CreatedAt,
                                    Customer = new Models.Models.Customer.CustomerDetailsModel
                                    {
                                        Id = salesOrder.CustomerNavigation.Id,
                                        FullName = salesOrder.CustomerNavigation.FullName,
                                        Email = salesOrder.CustomerNavigation.Email,
                                        PhoneNumber = salesOrder.CustomerNavigation.PhoneNumber,
                                        Group1 = salesOrder.CustomerNavigation.Group1,
                                        Group2 = salesOrder.CustomerNavigation.Group2,
                                        Group3 = salesOrder.CustomerNavigation.Group3,
                                        CreatedBy = salesOrder.CustomerNavigation.CreatedBy,
                                        CreatedAt = salesOrder.CustomerNavigation.CreatedAt,
                                        UpdatedAt = salesOrder.CustomerNavigation.UpdatedAt
                                    }
                                };

            return await qr_salesOrder.ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<SalesOrderDetailsModel>> List(int customerNumber)
        {
            var qr_salesOrder = from salesOrder in _storeCtx.SalesOrders
                                where salesOrder.Customer == customerNumber
                                select new SalesOrderDetailsModel
                                {
                                    DocNum = salesOrder.DocNum,
                                    DocDate = salesOrder.DocDate,
                                    DocStatus = salesOrder.DocStatus,
                                    DocTotal = salesOrder.DocTotal,
                                    MethodPayment = salesOrder.MethodPayment,
                                    Canceled = salesOrder.Canceled,
                                    CanceledBy = salesOrder.CanceledBy,
                                    CandeledDate = salesOrder.CandeledDate,
                                    CreatedBy = salesOrder.CreatedBy,
                                    UpdatedAt = salesOrder.UpdatedAt,
                                    CreatedAt = salesOrder.CreatedAt,
                                    Customer = new Models.Models.Customer.CustomerDetailsModel
                                    {
                                        Id = salesOrder.CustomerNavigation.Id,
                                        FullName = salesOrder.CustomerNavigation.FullName,
                                        Email = salesOrder.CustomerNavigation.Email,
                                        PhoneNumber = salesOrder.CustomerNavigation.PhoneNumber,
                                        Group1 = salesOrder.CustomerNavigation.Group1,
                                        Group2 = salesOrder.CustomerNavigation.Group2,
                                        Group3 = salesOrder.CustomerNavigation.Group3,
                                        CreatedBy = salesOrder.CustomerNavigation.CreatedBy,
                                        CreatedAt = salesOrder.CustomerNavigation.CreatedAt,
                                        UpdatedAt = salesOrder.CustomerNavigation.UpdatedAt
                                    }
                                };

            return await qr_salesOrder.ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(SalesOrderUpdateModel orderUpdateModel)
        {
            var orderRegistered = await _storeCtx.SalesOrders.FirstOrDefaultAsync(customer => customer.DocNum == orderUpdateModel.DocNum).ConfigureAwait(false);

            if (orderRegistered == null)
            {
                throw new NullReferenceException(nameof(orderRegistered));
            }

            orderRegistered.DocDate = orderUpdateModel.DocDate;
            orderRegistered.DocStatus = orderUpdateModel.DocStatus;
            orderRegistered.DocTotal = orderUpdateModel.DocTotal;
            orderRegistered.Customer = orderUpdateModel.Customer;
            orderRegistered.Canceled = orderUpdateModel.Canceled;
            orderRegistered.CanceledBy = orderUpdateModel.CanceledBy;
            orderRegistered.CandeledDate = orderUpdateModel.CandeledDate;
            orderRegistered.CandeledDate = orderUpdateModel.CandeledDate;
            orderRegistered.MethodPayment = orderUpdateModel.MethodPayment;
            orderRegistered.UpdatedAt = DateTime.Now;


            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateTotalAsync(int docNum)
        {
            var orderRegistered = await GetAsync(docNum).ConfigureAwait(false);

            orderRegistered.DocTotal = await _storeCtx.SalesOrderItems.Where(line => line.DocNum == docNum).SumAsync(line => line.Total).ConfigureAwait(false);
            orderRegistered.UpdatedAt = DateTime.Now;

            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<SalesOrder> GetAsync(int docNum)
        {
            var orderRegistered = await _storeCtx.SalesOrders.FirstOrDefaultAsync(customer => customer.DocNum == docNum).ConfigureAwait(false);

            if (orderRegistered == null)
            {
                throw new NullReferenceException(nameof(orderRegistered));
            }

            return orderRegistered;
        }
    }
}
