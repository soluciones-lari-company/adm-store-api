using Store.Models.Models.Customer;
using System;
using System.Collections.Generic;

namespace Store.Models.Models.SalesOrder
{
    public class SalesOrderCreateModel
    {
        public int Customer { get; set; }
        public DateTime DocDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal DocTotal { get; set; }
        public string DocStatus { get; set; } = null!;
    }

    public class SalesOrderDetailsModel
    {
        public int DocNum { get; set; }
        public Customer.CustomerDetailsModel Customer { get; set; } = null!;
        public DateTime DocDate { get; set; }
        public decimal DocTotal { get; set; }
        public string DocStatus { get; set; } = null!;
        public bool Canceled { get; set; }
        public DateTime CandeledDate { get; set; }
        public string CanceledBy { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string MethodPayment { get; set; }
    }
    public class SalesOrderDetailsFullModel
    {
        public SalesOrderDetailsModel Header { get; set; } = null!;
        public List<SalesOrderItemDetailsModel> Lines { get; set; } = null!;
    }

    public class SalesOrderUpdateModel
    {
        public int DocNum { get; set; }
        public int Customer { get; set; }
        public DateTime DocDate { get; set; }
        public decimal DocTotal { get; set; }
        public string DocStatus { get; set; } = null!;
        public bool Canceled { get; set; }
        public DateTime CandeledDate { get; set; }
        public string CanceledBy { get; set; } = null!;
        public string MethodPayment { get; set; }
    }
}
