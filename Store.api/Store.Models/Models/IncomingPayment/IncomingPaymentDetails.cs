using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Models.IncomingPayment
{
    public class IncomingPaymentDetailsModel
    {
        public int Id { get; set; }
        public Customer.CustomerDetailsModel Customer { get; set; }
        public int? DocNum { get; set; }
        public decimal Total { get; set; }
        public DateTime PaymentDate { get; set; }
        public BussinesAccountDetailsModel BussinesAccount { get; set; }
        public bool? Canceled { get; set; }
        public DateTime? CanceledDate { get; set; }
        public string CanceledBy { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class IncomingPaymentCreateModel
    {
        public int Customer { get; set; }
        public int? DocNum { get; set; }
        public decimal Total { get; set; }
        public DateTime PaymentDate { get; set; }
        public int BussinesAccount { get; set; }
        public string Comments { get; set; }
    }
}
