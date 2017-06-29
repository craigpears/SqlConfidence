using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class UserActionTypeMap : EntityTypeConfiguration<UserActionType>
    {
        public UserActionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.USAT_ID);

            // Properties
            this.Property(t => t.USAT_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESCRIPTION)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("USERS_ACTIONS_TYPES");
            this.Property(t => t.USAT_ID).HasColumnName("USAT_ID");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
        }
    }
}
