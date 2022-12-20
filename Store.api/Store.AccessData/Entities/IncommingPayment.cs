using System;
using System.Collections.Generic;

#nullable disable

namespace Store.AccessData.Entities
{
    public partial class IncommingPayment
    {
        public int Id { get; set; }
        public int Customer { get; set; }
        public int? DocNum { get; set; }
        public decimal Total { get; set; }
        public DateTime PaymentDate { get; set; }
        public int BussinesAccount { get; set; }
        public bool? Canceled { get; set; }
        public DateTime? CanceledDate { get; set; }
        public string CanceledBy { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual BussinesAccount BussinesAccountNavigation { get; set; }
        public virtual Customer CustomerNavigation { get; set; }
        public virtual SalesOrder DocNumNavigation { get; set; }
    }
}
