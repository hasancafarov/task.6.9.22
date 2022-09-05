using CardTask.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Application.Dtos
{
    public class AccountUpdateRequest
    {
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
    }

    public class CardUpdateRequest
    {
        public string CardNUmber { get; set; }
    }
}
