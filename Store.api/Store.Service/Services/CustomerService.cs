using Microsoft.Extensions.Logging;
using Store.AccessData.Interfaces;
using Store.Models.Models.Customer;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Group1 { get; set; }
        public int Group2 { get; set; }
        public int Group3 { get; set; }
        public string CreatedBy { get; internal set; } = null!;
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CustomerDetailsModel> GetAsync(int customerNumber)
        {
            var customerRegistered = await _customerRepository.DetailsAsync(customerNumber).ConfigureAwait(false);
            if (customerRegistered != null) MapModelToLocal(customerRegistered);
            return customerRegistered;
        }

        public async Task<CustomerDetailsModel?> GetAsync(string fullName)
        {
            var customerRegistered = await _customerRepository.ExistsAsync(fullName).ConfigureAwait(false);
            if (customerRegistered != null) MapModelToLocal(customerRegistered);
            return customerRegistered;
        }

        public async Task<List<CustomerDetailsModel>> ListAsync()
        {
            var listCustomers = await _customerRepository.ListAsync().ConfigureAwait(false);

            return listCustomers;
        }

        public async Task<int> CreateAsync(CustomerCreateModel customerCreate)
        {
            var idCustomerCreated = await _customerRepository.CreateAsync(customerCreate.FullName, customerCreate.PhoneNumber, customerCreate.Email).ConfigureAwait(false);

            return idCustomerCreated;
        }

        public async Task UpdateAsync(CustomerUpdateModel customerUpdate)
        {
            var customerRegistered = await _customerRepository.DetailsAsync(Id);

            if (customerRegistered is null) throw new ArgumentNullException(nameof(customerRegistered));

            if (customerRegistered.FullName != customerUpdate.FullName || customerRegistered.PhoneNumber != customerUpdate.PhoneNumber
                || customerRegistered.Email != customerUpdate.Email
                || customerRegistered.Group1 != customerUpdate.Group1
                || customerRegistered.Group2 != customerUpdate.Group2
                || customerRegistered.Group3 != customerUpdate.Group3)
            {
                await _customerRepository.UpdateAsync(customerUpdate).ConfigureAwait(false);

                customerRegistered = await _customerRepository.DetailsAsync(Id);
                if (customerRegistered != null) MapModelToLocal(customerRegistered);
            }
        }

        private void MapModelToLocal(CustomerDetailsModel customer)
        {
            if (customer is null) throw new ArgumentNullException(nameof(customer));
            Id = customer.Id;
            FullName = customer.FullName;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            Group1 = customer.Group1;
            Group2 = customer.Group2;
            Group3 = customer.Group3;
            CreatedBy = customer.CreatedBy;
            CreatedAt = customer.CreatedAt;
            UpdatedAt = customer.UpdatedAt;
        }
    }
}
