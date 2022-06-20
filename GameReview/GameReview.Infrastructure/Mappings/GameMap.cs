using GameReview.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReview.Infrastructure.Mappings
{
    public class GameMap : RegisterMap<Game>
    {
        public override void Configure(EntityTypeBuilder<Game> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Summary)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Developer)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Console)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasOne(x => x.GameGender)
                .WithMany()
                .HasForeignKey(x => x.GameGenderId)
                .IsRequired()
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
