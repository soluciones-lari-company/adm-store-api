using System;

namespace Store.Models.Models
{
    public class BussinesAccountDetailsModel
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public decimal Balance { get; set; }
        public string Comments { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
