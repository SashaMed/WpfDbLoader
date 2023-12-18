namespace B1Task2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankTransactionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankTransactions", "TransactionClass", c => c.String());
            DropColumn("dbo.BankTransactions", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankTransactions", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.BankTransactions", "TransactionClass");
        }
    }
}
