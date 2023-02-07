using GameReview.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReview.Infrastructure.Mappings
{
    public class ReviewMap : RegisterMap<Review>
    {
        public override void Configure(EntityTypeBuilder<Review> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.UserReview)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Score)
                .IsRequired()
                .HasMaxLength(10);

            builder
                .HasOne(x => x.Game)
                .WithMany()
                .HasForeignKey(x => x.GameId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
