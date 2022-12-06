using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Models
{
    public class BussinesAccountUpdateModel
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string Comments { get; set; } = null!;
    }
}
