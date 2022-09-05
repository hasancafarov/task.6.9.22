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
    public class Card:BaseEntity
    {
        [StringLength(16)]
        public string CardNumber { get; set; }//16
        public bool Valid { get; set; }
        public CardState State { get; set; }
        public CardType Type { get; set; }
        public Currency? Currency { get; set; }
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
