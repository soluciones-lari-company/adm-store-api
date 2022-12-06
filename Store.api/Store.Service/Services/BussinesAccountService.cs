using Microsoft.Extensions.Logging;
using Store.AccessData.Interfaces;
using Store.Models.Models;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.api")]
namespace Store.Service.Services
{
    internal class BussinesAccountService : IBussinesAccountService
    {
        private readonly IBussinesAccountRepository _bussinesAccountRepository;
        private readonly ILogger<BussinesAccountService> _logger;

        public BussinesAccountService(IBussinesAccountRepository bussinesAccountRepository, ILogger<BussinesAccountService> logger)
        {
            _bussinesAccountRepository = bussinesAccountRepository ?? throw new ArgumentNullException(nameof(bussinesAccountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateAsync(BussinesAccountCreateModel bussinesAccountCreate)
        {
            if (bussinesAccountCreate == null)
            {
                throw new ArgumentNullException(nameof(bussinesAccountCreate));
            }

            var idAccountCreated = await _bussinesAccountRepository.CreateAsync(bussinesAccountCreate.AccountName, bussinesAccountCreate.Comments).ConfigureAwait(false);

            return idAccountCreated;
        }

        public async Task<BussinesAccountDetailsModel> DetailsAsync(int idBussinesAccount)
        {
            if (idBussinesAccount == 0)
            {
                return null;
            }

            return await _bussinesAccountRepository.DetailsAsync(idBussinesAccount).ConfigureAwait(false);
        }

        public async Task<List<BussinesAccountHistoryDetailsModel>> GetHistory(int idBussinesAccount)
        {
            return await _bussinesAccountRepository.GetHistory(idBussinesAccount).ConfigureAwait(false);
        }

        public async Task<List<BussinesAccountDetailsModel>> ListAsync()
        {
            return await _bussinesAccountRepository.ListAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(BussinesAccountUpdateModel bussinesAccountUpdate)
        {
            var bussinesAccontRegistered = await _bussinesAccountRepository.DetailsAsync(bussinesAccountUpdate.Id).ConfigureAwait(false);

            if (bussinesAccontRegistered == null)
            {
                throw new ArgumentNullException(nameof(bussinesAccontRegistered));
            }

            if (bussinesAccontRegistered.Comments != bussinesAccountUpdate.Comments || bussinesAccontRegistered.AccountName != bussinesAccountUpdate.AccountName)
            {
                await _bussinesAccountRepository.UpdateAsync(bussinesAccountUpdate.Id, bussinesAccountUpdate.AccountName, bussinesAccountUpdate.Comments).ConfigureAwait(false);
            }
        }
    }
}
