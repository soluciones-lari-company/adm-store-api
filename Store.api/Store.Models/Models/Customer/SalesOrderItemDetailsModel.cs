using System;

namespace Store.Models.Models.Customer
{
    public class SalesOrderItemCreateModel
    {
        public int DocNum { get; set; }
        public string ItemCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int LineNum { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Comments { get; set; }
    }
    public class SalesOrderItemDetailsModel
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
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class SalesOrderItemUpdateModel
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
    }
}
