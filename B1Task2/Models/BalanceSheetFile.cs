using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task2.Models
{
    public class BalanceSheetFile
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
    }
}
