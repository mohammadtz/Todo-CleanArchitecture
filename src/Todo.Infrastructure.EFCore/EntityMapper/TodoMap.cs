using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Todo.Infrastructure.EFCore.EntityMapper;

public class TodoMap:IEntityTypeConfiguration<Domain.Todo.Todo>
{
    public void Configure(EntityTypeBuilder<Domain.Todo.Todo> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.CreationDate);

        builder.Property(e => e.IsDone);

        builder.Property(e => e.IsDoneDate);

        builder.Property(e => e.TagId);

        builder.HasOne(r=> r.Tag)
            .WithMany(p=> p.Tasks)
            .HasForeignKey(r => r.TagId);
    }
}