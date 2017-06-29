using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Mapping
{
    public class ExerciseQuestionChoiceMap : EntityTypeConfiguration<ExerciseQuestionChoice>
    {
        public ExerciseQuestionChoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.ExerciseQuestionChoiceId);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EXERCISES_QUESTIONS_CHOICES");
            this.Property(t => t.ExerciseQuestionChoiceId).HasColumnName("EXQC_ID");
            this.Property(t => t.ExerciseQuestionId).HasColumnName("EXQ_ID");
            this.Property(t => t.Description).HasColumnName("EXQC_DESCRIPTION");
            
            // Relationships
            this.HasRequired(t => t.ExerciseQuestion)
                .WithMany(t => t.ExerciseQuestionChoices)
                .HasForeignKey(d => d.ExerciseQuestionId);
        }        
    }
}
