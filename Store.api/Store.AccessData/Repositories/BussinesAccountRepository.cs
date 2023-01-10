using Microsoft.EntityFrameworkCore;
using Store.AccessData.Entities;
using Store.AccessData.Interfaces;
using Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Store.AccessData.Enums;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Repositories
{
    internal class BussinesAccountRepository : IBussinesAccountRepository
    {
        private readonly StoreDC _storeCtx;
        public int Id => _bussinesAccount.Id;
        public string AccountName
        {
            get { return _bussinesAccount.AccountName; }
            set { _bussinesAccount.AccountName = value; }
        }
        public decimal Balance
        {
            get { return _bussinesAccount.Balance; }
            set { _bussinesAccount.Balance = value; }
        }
        public string Comments
        {
            get { return _bussinesAccount.Comments; }
            set { _bussinesAccount.Comments = value; }
        }
        public bool DefaultAccount
        {
            get { return _bussinesAccount.DefaultAccount; }
            set { _bussinesAccount.DefaultAccount = value; }
        }
        public string CreatedBy
        {
            get { return _bussinesAccount.CreatedBy; }
            set { _bussinesAccount.CreatedBy = value; }
        }
        public DateTime CreatedAt
        {
            get { return _bussinesAccount.CreatedAt; }
            set { _bussinesAccount.CreatedAt = value; }
        }
        public DateTime UpdatedAt
        {
            get { return _bussinesAccount.UpdatedAt; }
            set { _bussinesAccount.UpdatedAt = value; }
        }
        public List<BussinesAccountHistoryDetailsModel> History
        {
            get 
            { 
                if(_bussinesAccount.BussinesAccountHistories == null)
                {
                    return new List<BussinesAccountHistoryDetailsModel>();
                }
                else
                {
                    var qr_list = from history in _bussinesAccount.BussinesAccountHistories
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
                                      Cancel = history.Cancel
                                  };
                    return qr_list.ToList();
                }
            }
        }
        private BussinesAccount _bussinesAccount = new BussinesAccount();

        public BussinesAccountRepository(StoreDC storeCtx)
        {
            _storeCtx = storeCtx;
        }

        public void Delete()
        {
            ValidLoadAccount();

            _storeCtx.BussinesAccounts.Remove(_bussinesAccount);
        }

        public async Task<BussinesAccountDetailsModel> GetAsync(int idIncomingPayment)
        {
            _bussinesAccount = await _storeCtx.BussinesAccounts.FirstOrDefaultAsync(account => account.Id == idIncomingPayment).ConfigureAwait(false);
            if(_bussinesAccount == null)
            {
                return null;
            }

            return new BussinesAccountDetailsModel
            {
                Id = _bussinesAccount.Id,
                AccountName = _bussinesAccount.AccountName,
                Balance = _bussinesAccount.Balance,
                Comments = _bussinesAccount.Comments,
                CreatedBy = _bussinesAccount.CreatedBy,
                CreatedAt = _bussinesAccount.CreatedAt,
                UpdatedAt = _bussinesAccount.UpdatedAt,
            };
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

        public async Task SaveAsync(SaveAction action = SaveAction.Create)
        {
            if(action == SaveAction.Create)
            {
                await _storeCtx.BussinesAccounts.AddAsync(_bussinesAccount);
            }
            else
            {
                _bussinesAccount.Balance = _bussinesAccount.BussinesAccountHistories.Where(line => line.Cancel == false || line.Cancel == null).Sum(line => line.Total);
            }
            await _storeCtx.SaveChangesAsync().ConfigureAwait(false);   
        }

        public void AddHistoryLine(decimal total, BussinesAccountHistoryType historyType, BussinesAccountDocRefType docRefType, int docRefNum, string comments)
        {
            ValidLoadAccount();
            var newHistoryLine = new BussinesAccountHistory
            {
                BussinesAccount = _bussinesAccount.Id,
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

            _bussinesAccount.BussinesAccountHistories.Add(newHistoryLine);
            //await _storeCtx.SaveChangesAsync().ConfigureAwait(false);

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

        private void ValidLoadAccount()
        {
            if (_bussinesAccount == null)
            {
                throw new NullReferenceException("The account selected has not been found");
            }
       }

        public async Task<BussinesAccountDetailsModel> GetDefaultAsync()
        {
            _bussinesAccount = await _storeCtx.BussinesAccounts.Include(p=> p.BussinesAccountHistories).FirstOrDefaultAsync(account => account.DefaultAccount == true).ConfigureAwait(false);
            if (_bussinesAccount == null)
            {
                return null;
            }

            return new BussinesAccountDetailsModel
            {
                Id = _bussinesAccount.Id,
                AccountName = _bussinesAccount.AccountName,
                Balance = _bussinesAccount.Balance,
                Comments = _bussinesAccount.Comments,
                CreatedBy = _bussinesAccount.CreatedBy,
                CreatedAt = _bussinesAccount.CreatedAt,
                UpdatedAt = _bussinesAccount.UpdatedAt,
            };
        }
    }
}
