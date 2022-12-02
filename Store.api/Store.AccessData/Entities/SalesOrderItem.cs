using System;
using System.Collections.Generic;

#nullable disable

namespace Store.AccessData.Entities
{
    public partial class SalesOrderItem
    {
        public int Id { get; set; }
        public int DocNum { get; set; }
        public string ItemCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int LineNum { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual SalesOrder DocNumNavigation { get; set; }
    }
}
