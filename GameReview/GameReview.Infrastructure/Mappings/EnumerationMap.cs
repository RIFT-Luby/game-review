using GameReview.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReview.Infrastructure.Mappings
{
    public class EnumerationMap<T> : IEntityTypeConfiguration<T> where T : Enumeration
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Name).IsRequired();
        }
    }
}
