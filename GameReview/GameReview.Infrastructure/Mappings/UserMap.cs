using GameReview.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReview.Infrastructure.Mappings
{
    public class UserMap : RegisterMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.UserName)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(x => x.UserRole)
                .WithMany()
                .HasForeignKey(x => x.UserRoleId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
