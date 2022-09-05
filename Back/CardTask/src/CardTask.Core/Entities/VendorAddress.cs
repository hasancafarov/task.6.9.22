using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Core.Entities
{
    public class VendorAddress : BaseEntity
    {
        [ForeignKey(nameof(Vendor))]
        public int VendorId { get; set; }
        public string Address { get; set; }
        public Vendor Vendor { get; set; }
    }
}
