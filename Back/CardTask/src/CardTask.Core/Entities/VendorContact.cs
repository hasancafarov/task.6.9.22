using CardTask.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Core.Entities
{
    public class VendorContact:BaseEntity
    {
        public string Contact { get; set; }
        public ContactType ContactType { get; set; }
        [ForeignKey(nameof(Vendor))]
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
