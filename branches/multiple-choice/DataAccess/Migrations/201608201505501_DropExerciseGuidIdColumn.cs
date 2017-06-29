namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropExerciseGuidIdColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ExerciseQuestions", "ExerciseQuestionIdGuid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExerciseQuestions", "ExerciseQuestionIdGuid", c => c.Guid(nullable: false));
        }
    }
}
