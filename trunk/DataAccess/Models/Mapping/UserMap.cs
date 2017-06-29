using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("USERS");
            this.Property(t => t.UserId).HasColumnName("US_ID");
            this.Property(t => t.FirstName).HasColumnName("US_FIRST_NAME");
            this.Property(t => t.LastName).HasColumnName("US_LAST_NAME");
            this.Property(t => t.Email).HasColumnName("US_EMAIL_ADDRESS");
            this.Property(t => t.Password).HasColumnName("US_PASSWORD");
            this.Property(t => t.CreatedDate).HasColumnName("US_CREATED_DATE");
            this.Property(t => t.CreatedBy).HasColumnName("US_CREATED_BY");
            this.Property(t => t.UpdatedDate).HasColumnName("US_UPDATED_DATE");
            this.Property(t => t.UpdatedBy).HasColumnName("US_UPDATED_BY");
            this.Property(t => t.Deleted).HasColumnName("US_DELETED");
            this.Property(t => t.DeletedDate).HasColumnName("US_DELETED_DATE");
            this.Property(t => t.DeletedBy).HasColumnName("US_DELETED_BY");
            this.Property(t => t.LastLogin).HasColumnName("US_LAST_LOGIN");
            this.Property(t => t.IsAdmin).HasColumnName("US_ADMIN_ACCESS");
        }
    }
}
