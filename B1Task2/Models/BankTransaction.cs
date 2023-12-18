using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task2.Models
{
    public class BankTransaction
    {
        public int BankTransactionId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string TransactionClass { get; set; }
        public decimal InitialActiveBalance { get; set; }
        public decimal InitialPassiveBalance { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal FinalActiveBalance { get; set; }
        public decimal FinalPassiveBalance { get; set; }
        public int FileId { get; set; } 
        public virtual BalanceSheetFile BalanceSheetFile { get; set; }
    }
}
