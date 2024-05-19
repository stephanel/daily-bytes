using Books.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.Persistence.Configurations;

internal class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Language).HasConversion<string>();
        builder.OwnsOne(c => c.Authors, d =>
        {
            d.ToJson();
        });

        // indexes
        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.ISBN).IsUnique();
        builder.HasIndex(x => x.Language);
        // no index on Authors for now
    }
}
