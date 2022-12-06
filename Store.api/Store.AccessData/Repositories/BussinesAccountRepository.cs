using Microsoft.EntityFrameworkCore;
using Store.AccessData.Entities;
using Store.AccessData.Interfaces;
using Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Repositories
{
    internal class BussinesAccountRepository : IBussinesAccountRepository
    {
        private readonly StoreDC _storeCtx;

        public BussinesAccountRepository(StoreDC storeCtx)
        {
            _storeCtx = storeCtx;
        }

        public async Task AddHistoryLine(int idBussinesAccount, decimal total, BussinesAccountHistoryType historyType, BussinesAccountDocRefType docRefType, int docRefNum, string comments)
        {
            var newHistoryLine = new BussinesAccountHistory
            {
                BussinesAccount = idBussinesAccount,
                Comments = comments,
                DocRefNum = docRefNum,
                DocRefType = MapBussinesAccountDocRefType(docRefType),
                Total = total,
                HistoryType = MapBussinesAccountHistoryType(historyType),
                // TODO service-user
                CreatedBy = "USER-SYS",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _storeCtx.BussinesAccountHistories.AddAsync(newHistoryLine).ConfigureAwait(false);
            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);
        }

        private string MapBussinesAccountHistoryType(BussinesAccountHistoryType historyType)
        {
            switch (historyType)
            {
                case BussinesAccountHistoryType.entrada:
                    return "I";
                case BussinesAccountHistoryType.salida:
                    return "O";
                default:
                    return "";
            }
        }

        private string MapBussinesAccountDocRefType(BussinesAccountDocRefType docRefType)
        {
            switch (docRefType)
            {
                case BussinesAccountDocRefType.incommingPayment:
                    return "I";
                case BussinesAccountDocRefType.outCommingPayment:
                    return "O";
                default:
                    return "";
            }
        }

        public async Task<int> CreateAsync(string name, string comments)
        {
            var newBussinesAccount = new BussinesAccount
            {
                AccountName = name,
                Comments = comments,
                Balance = 0,
                // TODO service-user
                CreatedBy = "USER-SYS",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _storeCtx.BussinesAccounts.AddAsync(newBussinesAccount).ConfigureAwait(false);
            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);

            return newBussinesAccount.Id;
        }

        public async Task<List<BussinesAccountDetailsModel>> ListAsync()
        {
            var qr_bussinesAccount = from account in _storeCtx.BussinesAccounts
                                     select new BussinesAccountDetailsModel
                                     {
                                         Id = account.Id,
                                         AccountName = account.AccountName,
                                         Balance = account.Balance,
                                         Comments = account.Comments,
                                         CreatedBy = account.CreatedBy,
                                         CreatedAt = account.CreatedAt,
                                         UpdatedAt = account.UpdatedAt,
                                     };

            return await qr_bussinesAccount.ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> UpdateAsync(int idBussinesAccount, string name, string comments)
        {
            var bussinesAccountRegistered = Get(idBussinesAccount);

            bussinesAccountRegistered.AccountName = name;
            bussinesAccountRegistered.Comments = comments;
            bussinesAccountRegistered.UpdatedAt = DateTime.Now;

            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);

            return bussinesAccountRegistered.Id;
        }

        public async Task<List<BussinesAccountHistoryDetailsModel>> GetHistory(int idBussinesAccoun)
        {
            var qr_history = from history in _storeCtx.BussinesAccountHistories
                             where history.BussinesAccount == idBussinesAccoun
                             select new BussinesAccountHistoryDetailsModel
                             {
                                 Id = history.Id,
                                 BussinesAccount = history.BussinesAccount,
                                 Total = history.Total,
                                 HistoryType = history.HistoryType,
                                 DocRefType = history.DocRefType,
                                 DocRefNum = history.DocRefNum,
                                 Comments = history.Comments,
                                 CreatedBy = history.CreatedBy,
                                 CreatedAt = history.CreatedAt,
                                 UpdatedAt = history.UpdatedAt,
                             };

            return await qr_history.ToListAsync().ConfigureAwait(false);
        }

        public async Task<BussinesAccountDetailsModel> DetailsAsync(int idBussinesAccount)
        {
            var qr_detailsAccount = from account in _storeCtx.BussinesAccounts
                                    where account.Id == idBussinesAccount
                                    select new BussinesAccountDetailsModel
                                    {
                                        Id = account.Id,
                                        AccountName = account.AccountName,
                                        Balance = account.Balance,
                                        Comments = account.Comments,
                                        CreatedBy = account.CreatedBy,
                                        CreatedAt = account.CreatedAt,
                                        UpdatedAt = account.UpdatedAt,
                                    };

            return await qr_detailsAccount.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public void SetAsDefault(int idBussinesAccount, bool isDefault)
        {
            var bussinesAccountRegistered = Get(idBussinesAccount);
            bussinesAccountRegistered.DefaultAccount = isDefault;
            _storeCtx.SaveChanges();
        }

        public async Task<List<BussinesAccountDetailsModel>> GetAsDefault()
        {
            var qr_bussinesAccount = from account in _storeCtx.BussinesAccounts where account.DefaultAccount == true
                                     select new BussinesAccountDetailsModel
                                     {
                                         Id = account.Id,
                                         AccountName = account.AccountName,
                                         Balance = account.Balance,
                                         Comments = account.Comments,
                                         CreatedBy = account.CreatedBy,
                                         CreatedAt = account.CreatedAt,
                                         UpdatedAt = account.UpdatedAt,
                                     };

            return await qr_bussinesAccount.ToListAsync().ConfigureAwait(false);
        }

        private BussinesAccount Get(int idBussinesAccount)
        {
            var bussinesAccountRegistered = _storeCtx.BussinesAccounts.FirstOrDefault(account => account.Id == idBussinesAccount);

            if (bussinesAccountRegistered == null)
            {
                throw new NullReferenceException(nameof(bussinesAccountRegistered));
            }

            return bussinesAccountRegistered;
        }
    }
}
