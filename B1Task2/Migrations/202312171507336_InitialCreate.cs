namespace B1Task2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BalanceSheetFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.BankTransactions",
                c => new
                    {
                        BankTransactionId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        AccountNumber = c.String(),
                        AccountName = c.String(),
                        InitialActiveBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InitialPassiveBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DebitAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalActiveBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalPassiveBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BankTransactionId)
                .ForeignKey("dbo.BalanceSheetFiles", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankTransactions", "FileId", "dbo.BalanceSheetFiles");
            DropIndex("dbo.BankTransactions", new[] { "FileId" });
            DropTable("dbo.BankTransactions");
            DropTable("dbo.BalanceSheetFiles");
        }
    }
}
