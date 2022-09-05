using CardTask.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal? Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        [StringLength(16)]
        public string CardNumber { get; set; }

        [ForeignKey(nameof(Vendor))]
        public int VendorId { get; set; }
        /*[ForeignKey(nameof(Account))]
        public int AccountId { get; set; }*/

        public Vendor Vendor { get; set; }
        //public Account Account { get; set; }
    }
}
