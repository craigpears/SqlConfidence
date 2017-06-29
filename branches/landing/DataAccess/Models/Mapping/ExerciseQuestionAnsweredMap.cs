using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class ExerciseQuestionAnsweredMap : EntityTypeConfiguration<ExerciseQuestionAnswered>
    {
        public ExerciseQuestionAnsweredMap()
        {
            // Primary Key
            this.HasKey(t => t.ExerciseQuestionAnsweredId);

            // Properties
            this.Property(t => t.UserId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("EXERCISES_QUESTIONS_ANSWERED");
            this.Property(t => t.ExerciseQuestionAnsweredId).HasColumnName("EXQA_ID");
            this.Property(t => t.UserId).HasColumnName("US_ID");
            this.Property(t => t.CreatedDate).HasColumnName("EXQA_CREATED_DATE");
            this.Property(t => t.ExerciseQuestionId).HasColumnName("EXQ_ID");

            // Relationships
            this.HasRequired(t => t.ExerciseQuestion)
                .WithMany(t => t.ExerciseQuestionAnswereds)
                .HasForeignKey(d => d.ExerciseQuestionId);

        }
    }
}
