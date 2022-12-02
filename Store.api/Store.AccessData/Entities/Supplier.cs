using System;
using System.Collections.Generic;

#nullable disable

namespace Store.AccessData.Entities
{
    public partial class Supplier
    {
        public Supplier()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public string CardCode { get; set; }
        public string SuplierName { get; set; }
        public string SupplierStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
