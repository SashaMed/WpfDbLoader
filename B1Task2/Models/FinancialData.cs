using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task2.Models
{
    internal class FinancialData
    {
        public string AccountNumber { get; set; }
        public string InitialActiveBalance { get; set; }
        public string InitialPassiveBalance { get; set; }
        public string DebitTurnover { get; set; }
        public string CreditTurnover { get; set; }
        public string FinalActiveBalance { get; set; }
        public string FinalPassiveBalance { get; set; }
    }
}
