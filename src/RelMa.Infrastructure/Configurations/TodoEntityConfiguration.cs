using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Todos;

namespace RelMa.Infrastructure.Configurations;

internal sealed class TodoItemConfiguration : IEntityTypeConfiguration<TodoEntity>
{
    public void Configure(EntityTypeBuilder<TodoEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.DueDate);
    }
}

