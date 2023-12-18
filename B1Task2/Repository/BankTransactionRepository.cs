using B1Task2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task2.Repository
{
    internal class BankTransactionRepository
    {
        ApplicationDbContext _dpContext;
        public BankTransactionRepository(ApplicationDbContext _dbContext)
        {
            _dpContext = _dbContext;
        }

        public List<BankTransaction> GetAll()
        {
            return _dpContext.BankTransactions.ToList();
        }

        public List<BankTransaction> GetByFileId(int fileId)
        {
            return _dpContext.BankTransactions.Where(c => c.FileId == fileId).ToList();
        }

        public void Create(List<BankTransaction> sheetFile, int fileId)
        {
            foreach (BankTransaction bankTransaction in sheetFile)
            {
                _dpContext.BankTransactions.Add(bankTransaction);
            }
            _dpContext.SaveChanges();
        }
    }
}
