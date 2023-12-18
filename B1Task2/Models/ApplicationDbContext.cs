using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1Task2.Models
{
    internal class ApplicationDbContext : DbContext
    {


        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<BalanceSheetFile> BalanceSheetFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure the primary key for the BankTransaction
            modelBuilder.Entity<BankTransaction>().HasKey(bt => bt.BankTransactionId);
            modelBuilder.Entity<BalanceSheetFile>().HasKey(bt => bt.FileId);
            // Define the foreign key from BankTransaction to BalanceSheetFile
            modelBuilder.Entity<BankTransaction>()
                        .HasRequired<BalanceSheetFile>(bt => bt.BalanceSheetFile)
                        .WithMany(bsf => bsf.BankTransactions)
                        .HasForeignKey(bt => bt.FileId);
        }
    }
}
