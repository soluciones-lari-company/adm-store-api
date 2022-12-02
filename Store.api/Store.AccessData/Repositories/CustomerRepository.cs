using Microsoft.EntityFrameworkCore;
using Store.AccessData.Entities;
using Store.AccessData.Interfaces;
using Store.Models.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly StoreDC _storeCtx;

        public CustomerRepository(StoreDC storeCtx)
        {
            _storeCtx = storeCtx;
        }

        public async Task<int> CreateAsync(string fullName, string phoneNumber, string emailAddress)
        {
            var newCustomer = new Customer
            {
                FullName = fullName,
                Email = emailAddress,
                PhoneNumber = phoneNumber,
                Group1 = 0,
                Group2 = 0,
                Group3 = 0,
                // TODO service-user
                CreatedBy = "USER-SYS",
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            };

            await _storeCtx.Customers.AddAsync(newCustomer).ConfigureAwait(false);
            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);

            return newCustomer.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var customerRegistered = await GetDetails(id).ConfigureAwait(false);

            _storeCtx.Customers.Remove(customerRegistered);

            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<CustomerDetailsModel?> DetailsAsync(int customerNumber)
        {
            var query_detailsCustomer = from customer in _storeCtx.Customers
                                        where customer.Id == customerNumber
                                        select new CustomerDetailsModel
                                        {
                                            Id = customerNumber,
                                            FullName = customer.FullName,
                                            Email = customer.Email,
                                            PhoneNumber = customer.PhoneNumber,
                                            Group1 = customer.Group1,
                                            Group2 = customer.Group2,
                                            Group3 = customer.Group3,
                                            CreatedBy = customer.CreatedBy,
                                            UpdatedAt = customer.UpdatedAt,
                                            CreatedAt = customer.UpdatedAt,
                                        };

            return await query_detailsCustomer.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<CustomerDetailsModel> ExistsAsync(string fullName)
        {
            var query_detailsCustomer = from customer in _storeCtx.Customers
                                        where customer.FullName == fullName
                                        select new CustomerDetailsModel
                                        {
                                            Id = customer.Id,
                                            FullName = customer.FullName,
                                            Email = customer.Email,
                                            PhoneNumber = customer.PhoneNumber,
                                            Group1 = customer.Group1,
                                            Group2 = customer.Group2,
                                            Group3 = customer.Group3,
                                            CreatedBy = customer.CreatedBy,
                                            UpdatedAt = customer.UpdatedAt,
                                            CreatedAt = customer.UpdatedAt,
                                        };
            return await query_detailsCustomer.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<List<CustomerDetailsModel>> ListAsync()
        {
            var query_detailsCustomer = from customer in _storeCtx.Customers
                                        select new CustomerDetailsModel
                                        {
                                            Id = customer.Id,
                                            FullName = customer.FullName,
                                            Email = customer.Email,
                                            PhoneNumber = customer.PhoneNumber,
                                            Group1 = customer.Group1,
                                            Group2 = customer.Group2,
                                            Group3 = customer.Group3,
                                            CreatedBy = customer.CreatedBy,
                                            UpdatedAt = customer.UpdatedAt,
                                            CreatedAt = customer.UpdatedAt,
                                        };

            return await query_detailsCustomer.ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(CustomerUpdateModel customerUpdate)
        {
            var customerRegistered = await GetDetails(customerUpdate.Id).ConfigureAwait(false);
            customerRegistered.FullName = customerUpdate.FullName;
            customerRegistered.Email = customerUpdate.Email;
            customerRegistered.PhoneNumber = customerUpdate.PhoneNumber;
            customerRegistered.Group1 = customerUpdate.Group1;
            customerRegistered.Group2 = customerUpdate.Group2;
            customerRegistered.Group3 = customerUpdate.Group3;
            customerRegistered.UpdatedAt = DateTime.Now;
            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<Customer> GetDetails(int customerNumber)
        {
            var customerRegistered = await _storeCtx.Customers.FirstOrDefaultAsync(customer => customer.Id == customerNumber).ConfigureAwait(false);

            if (customerRegistered == null)
            {
                throw new NullReferenceException(nameof(customerRegistered));
            }

            return customerRegistered;
        }
    }
}
