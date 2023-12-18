using B1Task2.Models;
using B1Task2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task2.Services
{
    internal class DataTransferService
    {
        ApplicationDbContext _dpContext;
        BalanceSheetFileRepository _fileRepository;
        BankTransactionRepository _transactionRepository;

        public DataTransferService(ApplicationDbContext _context)
        {
            _dpContext = _context;
            _fileRepository = new BalanceSheetFileRepository(_dpContext);
            _transactionRepository = new BankTransactionRepository(_dpContext);
        }

        public CreatedDbDto ImportFromSheetToDb(string filePath)
        {
            var reader = new ExcelReader();
            var fileRows = reader.ReadExcelFile(filePath);
            if (fileRows == null)
            {
                return null;
            }
            var sheetFile = CreateBalanceSheetFile(filePath);
            var bankTransactions = CreateBankTransactionsList(fileRows, sheetFile);
            _transactionRepository.Create(bankTransactions, sheetFile.FileId);
            var dto = new CreatedDbDto
            {
                finDatas = fileRows,
                fileData = ConverToFileTableData(sheetFile)
            };
            return dto;
        }

        private BalanceSheetFile CreateBalanceSheetFile(string filePath)
        {
            var file = new BalanceSheetFile
            {
                FileName = filePath,
                UploadDate = DateTime.Now,
            };
            _fileRepository.Create(file);
            return file;
        }

        private List<BankTransaction> CreateBankTransactionsList(List<FinancialData> rows, BalanceSheetFile sheetFile)
        {
            var bankTransactions = new List<BankTransaction>();
            var currentClass = rows[0].AccountNumber;
            for (int i = 0; i < rows.Count; i++)
            {
                var bankTransaction = ConvertToBankTransaction(rows[i], currentClass, sheetFile.FileId);
                if (bankTransaction != null)
                {
                    bankTransactions.Add(bankTransaction);
                }
            }
            return bankTransactions;
        }

        private BankTransaction ConvertToBankTransaction(FinancialData data, string transactionClass, int fileId)
        {
            var transaction = new BankTransaction();
            try
            {
                transaction.FileId = fileId;
                transaction.AccountNumber = data.AccountNumber;
                transaction.TransactionClass = transactionClass;
                transaction.InitialActiveBalance = decimal.Parse(data.InitialActiveBalance);
                transaction.InitialPassiveBalance = decimal.Parse(data.InitialPassiveBalance);
                transaction.DebitAmount = decimal.Parse(data.DebitTurnover);
                transaction.CreditAmount = decimal.Parse(data.CreditTurnover);
                transaction.FinalActiveBalance = decimal.Parse(data.FinalActiveBalance);
                transaction.FinalPassiveBalance = decimal.Parse(data.FinalPassiveBalance);

                return transaction;
            }
            catch (Exception ex)
            {
                return transaction;
            }
        }

        public List<FileTableData> GetFilesData()
        {
            var files = new List<FileTableData>();
            var filesData = _fileRepository.GetAll();
            foreach (var file in filesData)
            {
                var converted = ConverToFileTableData(file);
                if (converted != null)
                {
                    files.Add(converted);
                }
            }
            return files;
        }

        private FileTableData ConverToFileTableData(BalanceSheetFile balanceSheet)
        {
            return new FileTableData
            {
                FileId = balanceSheet.FileId,
                FileName = balanceSheet.FileName,
                Date = balanceSheet.UploadDate.ToString("g")
            };
        }

        public List<FinancialData> GetFinancialDatas(int fileId)
        {
            var finDatas = new List<FinancialData>();
            _transactionRepository.GetByFileId(fileId).ToList().ForEach(transaction => 
            { 
                var finData = ConvertToFinancialDatas(transaction); 
                if (finData != null)
                {
                    finDatas.Add(finData);
                }
            });
            return finDatas;
        }

        private FinancialData ConvertToFinancialDatas(BankTransaction transaction)
        {
            var data = new FinancialData();
            data.AccountNumber = transaction.AccountNumber;
            data.InitialActiveBalance = transaction.InitialActiveBalance.ToString();
            data.InitialPassiveBalance = transaction.InitialPassiveBalance.ToString();
            data.DebitTurnover = transaction.DebitAmount.ToString();
            data.CreditTurnover = transaction.CreditAmount.ToString();
            data.FinalActiveBalance = transaction.FinalActiveBalance.ToString();
            data.FinalPassiveBalance = transaction.FinalPassiveBalance.ToString();
            return data;
        }

        public void DeleteAll()
        {
            _fileRepository.DeleteAll();
        }
    }
}
