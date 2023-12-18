using B1Task2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task2.Repository
{
    internal class BalanceSheetFileRepository
    {
        ApplicationDbContext _dpContext;
        public BalanceSheetFileRepository(ApplicationDbContext _dbContext) 
        {
            _dpContext = _dbContext;
        }

        public List<BalanceSheetFile> GetAll()
        {
            return _dpContext.BalanceSheetFiles.ToList();
        }

        public BalanceSheetFile Create(BalanceSheetFile sheetFile)
        {
            var ret = _dpContext.BalanceSheetFiles.Add(sheetFile);
            _dpContext.SaveChanges();
            return ret;
        }

        public void DeleteAll()
        {
            var rows = _dpContext.BalanceSheetFiles.ToList();
            foreach (var row in rows)
            {
                _dpContext.BalanceSheetFiles.Remove(row);
            }
            _dpContext.SaveChanges();
        }

    }
}
