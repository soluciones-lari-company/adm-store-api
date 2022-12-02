using System;
using System.Collections.Generic;

#nullable disable

namespace Store.AccessData.Entities
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderItems = new HashSet<SalesOrderItem>();
        }

        public int DocNum { get; set; }
        public int Customer { get; set; }
        public DateTime DocDate { get; set; }
        public decimal DocTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal DocTotalFinal { get; set; }
        public string DocStatus { get; set; }
        public string MethodPayment { get; set; }
        public bool Canceled { get; set; }
        public DateTime CandeledDate { get; set; }
        public string CanceledBy { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Customer CustomerNavigation { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
