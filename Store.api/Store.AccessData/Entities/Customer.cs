using System;
using System.Collections.Generic;

#nullable disable

namespace Store.AccessData.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Group1 { get; set; }
        public int Group2 { get; set; }
        public int Group3 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
