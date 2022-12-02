using System;
using System.Collections.Generic;

#nullable disable

namespace Store.AccessData.Entities
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
        }

        public int DocNum { get; set; }
        public string Supplier { get; set; }
        public DateTime DocDate { get; set; }
        public decimal DocTotal { get; set; }
        public string DocStatus { get; set; }
        public bool Canceled { get; set; }
        public DateTime? CandeledDate { get; set; }
        public string CanceledBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Supplier SupplierNavigation { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
