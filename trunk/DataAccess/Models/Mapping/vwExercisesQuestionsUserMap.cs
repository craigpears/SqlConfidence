using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class vwExercisesQuestionsUserMap : EntityTypeConfiguration<vwExercisesQuestionsUser>
    {
        public vwExercisesQuestionsUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EXQ_ID, t.EX_ID, t.EXQ_INSTRUCTIONS_TEMPLATE, t.EXQ_ANSWERS_TEMPLATE, t.EXQ_DESCRIPTION, t.EXQ_CREATED_BY, t.EXQ_CREATED_DATE, t.EXQ_DELETED, t.EXQ_ID_GUID, t.us_id });

            // Properties
            this.Property(t => t.EXQ_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EX_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EXQ_INSTRUCTIONS_TEMPLATE)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.EXQ_ANSWERS_TEMPLATE)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.EXQ_DESCRIPTION)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.EXQ_CREATED_BY)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EXQ_UPDATED_BY)
                .HasMaxLength(50);

            this.Property(t => t.EXQ_DELETED_BY)
                .HasMaxLength(50);

            this.Property(t => t.EXQ_HINT)
                .HasMaxLength(500);

            this.Property(t => t.EXQ_STARTING_SQL)
                .HasMaxLength(2000);

            this.Property(t => t.us_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwExercisesQuestionsUsers");
            this.Property(t => t.EXQ_ID).HasColumnName("EXQ_ID");
            this.Property(t => t.EX_ID).HasColumnName("EX_ID");
            this.Property(t => t.EXQ_INSTRUCTIONS_TEMPLATE).HasColumnName("EXQ_INSTRUCTIONS_TEMPLATE");
            this.Property(t => t.EXQ_ANSWERS_TEMPLATE).HasColumnName("EXQ_ANSWERS_TEMPLATE");
            this.Property(t => t.EXQ_DESCRIPTION).HasColumnName("EXQ_DESCRIPTION");
            this.Property(t => t.EXQ_CREATED_BY).HasColumnName("EXQ_CREATED_BY");
            this.Property(t => t.EXQ_CREATED_DATE).HasColumnName("EXQ_CREATED_DATE");
            this.Property(t => t.EXQ_UPDATED_BY).HasColumnName("EXQ_UPDATED_BY");
            this.Property(t => t.EXQ_UPDATED_DATE).HasColumnName("EXQ_UPDATED_DATE");
            this.Property(t => t.EXQ_DELETED).HasColumnName("EXQ_DELETED");
            this.Property(t => t.EXQ_DELETED_BY).HasColumnName("EXQ_DELETED_BY");
            this.Property(t => t.EXQ_DELETED_DATE).HasColumnName("EXQ_DELETED_DATE");
            this.Property(t => t.EXQ_HINT).HasColumnName("EXQ_HINT");
            this.Property(t => t.EXQ_ORDER).HasColumnName("EXQ_ORDER");
            this.Property(t => t.EXQ_STARTING_SQL).HasColumnName("EXQ_STARTING_SQL");
            this.Property(t => t.EXQ_ID_GUID).HasColumnName("EXQ_ID_GUID");
            this.Property(t => t.us_id).HasColumnName("us_id");
            this.Property(t => t.AnsweredDate).HasColumnName("AnsweredDate");
            this.Property(t => t.Answered).HasColumnName("Answered");
        }
    }
}
