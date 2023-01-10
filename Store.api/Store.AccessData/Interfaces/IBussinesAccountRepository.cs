using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Models.Models;
using System.Runtime.CompilerServices;
using System;
using Store.AccessData.Enums;

[assembly: InternalsVisibleTo("Store.Service")]
namespace Store.AccessData.Interfaces
{
    internal interface IBussinesAccountRepository
    {
        public int Id { get;}
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string Comments { get; set; }
        public bool DefaultAccount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BussinesAccountHistoryDetailsModel> History { get; }
        public void Delete();
        public Task<BussinesAccountDetailsModel> GetAsync(int idIncomingPayment);
        public Task<BussinesAccountDetailsModel> GetDefaultAsync();
        public Task<List<BussinesAccountDetailsModel>> ListAsync();
        public Task SaveAsync(SaveAction action = SaveAction.Create);
        public void AddHistoryLine(decimal total, BussinesAccountHistoryType historyType, BussinesAccountDocRefType docRefType, int docRefNum, string comments);
    }

    internal enum BussinesAccountHistoryType
    {
        entrada = 1,
        salida = 2
    }

    internal enum BussinesAccountDocRefType
    {
        incommingPayment = 1,
        outCommingPayment = 2
    }
}
