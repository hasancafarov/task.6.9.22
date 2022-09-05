using CardTask.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Application.Dtos
{
    public class AccountCreateRequest
    {
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }

    }
    public class CardCreateRequest
    {
        public string Cardnumber { get; set; }
        public string UserId { get; set; }

    }
}
