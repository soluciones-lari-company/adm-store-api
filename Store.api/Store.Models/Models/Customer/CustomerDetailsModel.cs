using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Models.Models.Customer
{
    public class CustomerCreateModel
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; }
        public int Group1 { get; set; }
        public int Group2 { get; set; }
        public int Group3 { get; set; }

    }

    public class CustomerDetailsModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; }
        public int Group1 { get; set; }
        public int Group2 { get; set; }
        public int Group3 { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class CustomerUpdateModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; }
        public int Group1 { get; set; }
        public int Group2 { get; set; }
        public int Group3 { get; set; }
    }
}
