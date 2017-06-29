namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FakeMigration : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions", "MultipleChoiceDataQuery_Id", "dbo.MultipleChoiceDataQueries");
            //DropPrimaryKey("dbo.MultipleChoiceDataQueries");
            //AlterColumn("dbo.MultipleChoiceDataQueries", "Id", c => c.Guid(nullable: false, identity: true));
            //AddPrimaryKey("dbo.MultipleChoiceDataQueries", "Id");
            //AddForeignKey("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions", "MultipleChoiceDataQuery_Id", "dbo.MultipleChoiceDataQueries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions", "MultipleChoiceDataQuery_Id", "dbo.MultipleChoiceDataQueries");
            DropPrimaryKey("dbo.MultipleChoiceDataQueries");
            AlterColumn("dbo.MultipleChoiceDataQueries", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.MultipleChoiceDataQueries", "Id");
            AddForeignKey("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions", "MultipleChoiceDataQuery_Id", "dbo.MultipleChoiceDataQueries", "Id", cascadeDelete: true);
        }
    }
}
