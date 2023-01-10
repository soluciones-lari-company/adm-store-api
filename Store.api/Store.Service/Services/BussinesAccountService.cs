using Microsoft.Extensions.Logging;
using Store.AccessData.Interfaces;
using Store.Models.Models;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq;

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
            _bussinesAccountRepository.AccountName = bussinesAccountCreate.AccountName;
            _bussinesAccountRepository.Comments = bussinesAccountCreate.Comments;
            _bussinesAccountRepository.DefaultAccount = false;
            _bussinesAccountRepository.Balance = 0;
            // TODO service-user
            _bussinesAccountRepository.CreatedBy = "USER-SYS";
            _bussinesAccountRepository.CreatedAt = DateTime.Now;
            _bussinesAccountRepository.UpdatedAt = DateTime.Now;

            await _bussinesAccountRepository.SaveAsync();

            return _bussinesAccountRepository.Id;
        }

        public async Task<BussinesAccountDetailsModel> DetailsAsync(int idBussinesAccount)
        {
            var accountDetails =  await _bussinesAccountRepository.GetAsync(idBussinesAccount).ConfigureAwait(false);

            return accountDetails;
        }

        public async Task<List<BussinesAccountHistoryDetailsModel>> GetHistory(int idBussinesAccount)
        {
            await _bussinesAccountRepository.GetAsync(idBussinesAccount).ConfigureAwait(false);

            return _bussinesAccountRepository.History;
        }

        public async Task<List<BussinesAccountDetailsModel>> ListAsync()
        {
            var listAccounts = await _bussinesAccountRepository.ListAsync().ConfigureAwait(false);

            return listAccounts;
        }

        public async Task UpdateAsync(BussinesAccountUpdateModel bussinesAccountUpdate)
        {
            var bussinesAccontRegistered = await _bussinesAccountRepository.GetAsync(bussinesAccountUpdate.Id).ConfigureAwait(false);

            if (bussinesAccontRegistered == null)
            {
                throw new ArgumentNullException(nameof(bussinesAccontRegistered));
            }

            if (bussinesAccontRegistered.Comments != bussinesAccountUpdate.Comments || bussinesAccontRegistered.AccountName != bussinesAccountUpdate.AccountName)
            {
                bussinesAccountUpdate.AccountName = bussinesAccountUpdate.AccountName;
                bussinesAccountUpdate.Comments = bussinesAccountUpdate.Comments;
            }

            await _bussinesAccountRepository.SaveAsync();
        }

        public async Task AddHistoryLine(int idBussinesAccount, BussinesAccountHistoryType historyType, BussinesAccountDocRefType docRefType, int docRefNum, string comments)
        {
            var bussinesAccontRegistered = await _bussinesAccountRepository.GetAsync(idBussinesAccount).ConfigureAwait(false);

            if (bussinesAccontRegistered == null)
            {
                throw new ArgumentNullException(nameof(bussinesAccontRegistered));
            }
            _bussinesAccountRepository.AddHistoryLine(0, historyType, docRefType, docRefNum, comments);
            _bussinesAccountRepository.Balance = _bussinesAccountRepository.History.Sum(line => line.Total);
            await _bussinesAccountRepository.SaveAsync();
        }
    }
}
