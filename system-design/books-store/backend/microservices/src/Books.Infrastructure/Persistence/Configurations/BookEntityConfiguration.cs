using Books.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.Persistence.Configurations;

internal class BookEntityConfiguration : IEntityTypeConfiguration<BookDb>
{
    public void Configure(EntityTypeBuilder<BookDb> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(c => c.Payload, d =>
        {
            d.ToJson();
            d.OwnsMany(x => x.Authors);
        });

        // indexes
        builder.HasIndex(x => x.Title);
    }

   
}
