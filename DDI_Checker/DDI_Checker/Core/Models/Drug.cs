using System;
using System.Collections.Generic;
using System.Text;

namespace DDI_Checker.Core.Models
{
    internal class Drug
    {
        public string Name { get;  }
        public string DrugBankId { get;  }

        public Drug(string name, string drugBankId)
        {
            Name = name;
            DrugBankId = drugBankId;
        }
    }
}
