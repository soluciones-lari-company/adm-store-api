using System;
using System.Collections.Generic;

#nullable disable

namespace Store.AccessData.Entities
{
    public partial class PurchaseOrderItem
    {
        public int Id { get; set; }
        public int? DocNum { get; set; }
        public string ItemCode { get; set; }
        public string DescriptionItem { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int LineNum { get; set; }
        public string ItemBy { get; set; }
        public decimal WeightItem { get; set; }
        public decimal PriceByGrs { get; set; }
        public decimal FactorRevenue { get; set; }
        public decimal PublicPrice { get; set; }
        public bool IsSold { get; set; }
        public string Reference1 { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual PurchaseOrder DocNumNavigation { get; set; }
    }
}
