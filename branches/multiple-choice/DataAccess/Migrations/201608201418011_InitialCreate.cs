namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataSources",
                c => new
                    {
                        DataSourceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DataSourceIdGuid = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 50),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 50),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.String(maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DataSourceId);
            
            CreateTable(
                "dbo.DataSourceTables",
                c => new
                    {
                        DataSourceTableId = c.Int(nullable: false, identity: true),
                        DataSourceId = c.Int(nullable: false),
                        TableName = c.String(),
                        TableAlias = c.String(),
                        Columns = c.String(),
                        DataSourceTableIdGuid = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 50),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 50),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.String(maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DataSourceTableId)
                .ForeignKey("dbo.DataSources", t => t.DataSourceId, cascadeDelete: true)
                .Index(t => t.DataSourceId);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseId = c.Int(nullable: false, identity: true),
                        DataSourceId = c.Int(nullable: false),
                        Name = c.String(),
                        Summary = c.String(),
                        Published = c.Boolean(),
                        PublishedDate = c.DateTime(),
                        PublishedBy = c.String(),
                        SectionName = c.String(),
                        Order = c.Int(nullable: false),
                        ShowQueryBuilder = c.Boolean(),
                        ExerciseIdGuid = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 50),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 50),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.String(maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ExerciseId)
                .ForeignKey("dbo.DataSources", t => t.DataSourceId, cascadeDelete: true)
                .Index(t => t.DataSourceId);
            
            CreateTable(
                "dbo.ExerciseQuestions",
                c => new
                    {
                        ExerciseQuestionId = c.Int(nullable: false, identity: true),
                        ExerciseId = c.Int(nullable: false),
                        InstructionsTemplate = c.String(),
                        AnswerTemplate = c.String(),
                        Description = c.String(),
                        Hint = c.String(),
                        Order = c.Int(),
                        StartingSql = c.String(),
                        ExerciseQuestionIdGuid = c.Guid(nullable: false),
                        ExerciseQuestionType = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 50),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 50),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.String(maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        CorrectOption_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.ExerciseQuestionId)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .ForeignKey("dbo.MultipleChoiceOptions", t => t.CorrectOption_Id)
                .Index(t => t.ExerciseId)
                .Index(t => t.CorrectOption_Id);
            
            CreateTable(
                "dbo.ExerciseQuestionAnswereds",
                c => new
                    {
                        ExerciseQuestionAnsweredId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ExerciseQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseQuestionAnsweredId)
                .ForeignKey("dbo.ExerciseQuestions", t => t.ExerciseQuestionId, cascadeDelete: true)
                .Index(t => t.ExerciseQuestionId);
            
            CreateTable(
                "dbo.ExerciseQuestionChoices",
                c => new
                    {
                        ExerciseQuestionChoiceId = c.Guid(nullable: false),
                        ExerciseQuestionId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ExerciseQuestionChoiceId)
                .ForeignKey("dbo.ExerciseQuestions", t => t.ExerciseQuestionId, cascadeDelete: true)
                .Index(t => t.ExerciseQuestionId);
            
            CreateTable(
                "dbo.ExerciseQuestionUnitTests",
                c => new
                    {
                        ExerciseQuestionUnitTestId = c.Guid(nullable: false),
                        ExerciseQuestionId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        SqlToRun = c.String(),
                        SqlToCompare = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 50),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 50),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.String(maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseQuestionUnitTestId)
                .ForeignKey("dbo.ExerciseQuestions", t => t.ExerciseQuestionId, cascadeDelete: true)
                .Index(t => t.ExerciseQuestionId);
            
            CreateTable(
                "dbo.MultipleChoiceOptions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ExerciseQuestionId = c.Int(nullable: false),
                        Description = c.String(maxLength: 20),
                        CorrectAnswerMessage = c.String(),
                        IncorrectAnswerMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExerciseQuestions", t => t.ExerciseQuestionId, cascadeDelete: true)
                .Index(t => t.ExerciseQuestionId);
            
            CreateTable(
                "dbo.MultipleChoiceDataQueries",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ExerciseId = c.Int(nullable: false),
                        SqlQuery = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .Index(t => t.ExerciseId);
            
            CreateTable(
                "dbo.UserActions",
                c => new
                    {
                        USA_ID = c.Int(nullable: false, identity: true),
                        EXQ_ID = c.Int(),
                        US_ID = c.Int(nullable: false),
                        USA_DESCRIPTION = c.String(),
                        USAT_ID = c.Int(nullable: false),
                        USA_CREATED_DATE = c.DateTime(nullable: false),
                        USA_RESET_DATE = c.DateTime(),
                        USERS_ACTIONS_TYPES_Id = c.Int(),
                    })
                .PrimaryKey(t => t.USA_ID)
                .ForeignKey("dbo.UserActionTypes", t => t.USERS_ACTIONS_TYPES_Id)
                .Index(t => t.USERS_ACTIONS_TYPES_Id);
            
            CreateTable(
                "dbo.UserActionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        LastLogin = c.DateTime(),
                        IsAdmin = c.Boolean(nullable: false),
                        IsGuest = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 50),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 50),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.String(maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.MultipleChoiceDataQueryMultipleChoiceQuestions",
                c => new
                    {
                        MultipleChoiceDataQuery_Id = c.Guid(nullable: false),
                        MultipleChoiceQuestion_ExerciseQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MultipleChoiceDataQuery_Id, t.MultipleChoiceQuestion_ExerciseQuestionId })
                .ForeignKey("dbo.MultipleChoiceDataQueries", t => t.MultipleChoiceDataQuery_Id, cascadeDelete: false)
                .ForeignKey("dbo.ExerciseQuestions", t => t.MultipleChoiceQuestion_ExerciseQuestionId, cascadeDelete: true)
                .Index(t => t.MultipleChoiceDataQuery_Id)
                .Index(t => t.MultipleChoiceQuestion_ExerciseQuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserActions", "USERS_ACTIONS_TYPES_Id", "dbo.UserActionTypes");
            DropForeignKey("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions", "MultipleChoiceQuestion_ExerciseQuestionId", "dbo.ExerciseQuestions");
            DropForeignKey("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions", "MultipleChoiceDataQuery_Id", "dbo.MultipleChoiceDataQueries");
            DropForeignKey("dbo.MultipleChoiceDataQueries", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.ExerciseQuestions", "CorrectOption_Id", "dbo.MultipleChoiceOptions");
            DropForeignKey("dbo.MultipleChoiceOptions", "ExerciseQuestionId", "dbo.ExerciseQuestions");
            DropForeignKey("dbo.ExerciseQuestionUnitTests", "ExerciseQuestionId", "dbo.ExerciseQuestions");
            DropForeignKey("dbo.ExerciseQuestionChoices", "ExerciseQuestionId", "dbo.ExerciseQuestions");
            DropForeignKey("dbo.ExerciseQuestionAnswereds", "ExerciseQuestionId", "dbo.ExerciseQuestions");
            DropForeignKey("dbo.ExerciseQuestions", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "DataSourceId", "dbo.DataSources");
            DropForeignKey("dbo.DataSourceTables", "DataSourceId", "dbo.DataSources");
            DropIndex("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions", new[] { "MultipleChoiceQuestion_ExerciseQuestionId" });
            DropIndex("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions", new[] { "MultipleChoiceDataQuery_Id" });
            DropIndex("dbo.UserActions", new[] { "USERS_ACTIONS_TYPES_Id" });
            DropIndex("dbo.MultipleChoiceDataQueries", new[] { "ExerciseId" });
            DropIndex("dbo.MultipleChoiceOptions", new[] { "ExerciseQuestionId" });
            DropIndex("dbo.ExerciseQuestionUnitTests", new[] { "ExerciseQuestionId" });
            DropIndex("dbo.ExerciseQuestionChoices", new[] { "ExerciseQuestionId" });
            DropIndex("dbo.ExerciseQuestionAnswereds", new[] { "ExerciseQuestionId" });
            DropIndex("dbo.ExerciseQuestions", new[] { "CorrectOption_Id" });
            DropIndex("dbo.ExerciseQuestions", new[] { "ExerciseId" });
            DropIndex("dbo.Exercises", new[] { "DataSourceId" });
            DropIndex("dbo.DataSourceTables", new[] { "DataSourceId" });
            DropTable("dbo.MultipleChoiceDataQueryMultipleChoiceQuestions");
            DropTable("dbo.Users");
            DropTable("dbo.UserActionTypes");
            DropTable("dbo.UserActions");
            DropTable("dbo.MultipleChoiceDataQueries");
            DropTable("dbo.MultipleChoiceOptions");
            DropTable("dbo.ExerciseQuestionUnitTests");
            DropTable("dbo.ExerciseQuestionChoices");
            DropTable("dbo.ExerciseQuestionAnswereds");
            DropTable("dbo.ExerciseQuestions");
            DropTable("dbo.Exercises");
            DropTable("dbo.DataSourceTables");
            DropTable("dbo.DataSources");
        }
    }
}
