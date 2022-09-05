using CardTask.Core.Enums;
using CardTask.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Core.Entities
{
    public class Account : BaseEntity
    {
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
        public User User { get; set; }
        
        public IEnumerable<Transaction> Transactions { get; set; }
        public Card Card { get; set; }
        public bool isDefault { get; set; }
    }
}
