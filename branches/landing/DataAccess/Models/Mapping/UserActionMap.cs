using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class UserActionMap : EntityTypeConfiguration<UserAction>
    {
        public UserActionMap()
        {
            // Primary Key
            this.HasKey(t => t.USA_ID);

            // Properties
            this.Property(t => t.USA_DESCRIPTION)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("USERS_ACTIONS");
            this.Property(t => t.USA_ID).HasColumnName("USA_ID");
            this.Property(t => t.EXQ_ID).HasColumnName("EXQ_ID");
            this.Property(t => t.US_ID).HasColumnName("US_ID");
            this.Property(t => t.USA_DESCRIPTION).HasColumnName("USA_DESCRIPTION");
            this.Property(t => t.USAT_ID).HasColumnName("USAT_ID");
            this.Property(t => t.USA_CREATED_DATE).HasColumnName("USA_CREATED_DATE");
            this.Property(t => t.USA_RESET_DATE).HasColumnName("USA_RESET_DATE");

            // Relationships
            this.HasRequired(t => t.USERS_ACTIONS_TYPES)
                .WithMany(t => t.USERS_ACTIONS)
                .HasForeignKey(d => d.USAT_ID);

        }
    }
}
