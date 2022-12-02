using System;

namespace Store.Models.Models.PurchaseOrder
{
    public class PurchaseOrderItemDetailsModel
    {
        public string TypeItem { get; set; } = null!;
        public string ItemCode { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal PublicPrice { get; set; }
        public int LineNum { get; set; }
        public string? Reference1 { get; set; }
        public string? Reference2 { get; set; }
        public decimal WeightItem { get; set; }
        public decimal PriceByGrs { get; set; }
        public decimal PriceSale { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal FactorRevenue { get; set; }
        public string? Comments { get; set; }
        public string CreatedBy { get; set; } = null!;
        public bool IsSold { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string DescriptionItem { get; set; }

        public PurchaseOrderItemDetailsModel()
        {
            PriceSale = UnitPrice * FactorRevenue;
            TotalRevenue = PriceSale - UnitPrice;
        }
    }
}
