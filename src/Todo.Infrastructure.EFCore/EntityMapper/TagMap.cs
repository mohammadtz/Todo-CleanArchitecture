using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Tag;

namespace Todo.Infrastructure.EFCore.EntityMapper;

public class TagMap:IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired();

        builder.Property(e => e.Color);

        builder.Property(e => e.CreationDate);
    }
}