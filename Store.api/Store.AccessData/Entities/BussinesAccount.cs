using System;
using System.Collections.Generic;

#nullable disable

namespace Store.AccessData.Entities
{
    public partial class BussinesAccount
    {
        public BussinesAccount()
        {
            BussinesAccountHistories = new HashSet<BussinesAccountHistory>();
            IncommingPayments = new HashSet<IncommingPayment>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string Comments { get; set; }
        public bool DefaultAccount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<BussinesAccountHistory> BussinesAccountHistories { get; set; }
        public virtual ICollection<IncommingPayment> IncommingPayments { get; set; }
    }
}
