using CardTask.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Core.Entities
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<VendorAddress> VendorAddresses { get; set; }
        public IEnumerable<VendorContact> VendorContacts { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
