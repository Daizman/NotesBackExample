using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Persistent.EntityTypeConfigurations
{
    public class NoteConfig : IEntityTypeConfiguration<Note>
    {
        // Конфигурация для типа сущности
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(note => note.Id);
            builder.HasIndex(note => note.Id).IsUnique();
            builder.Property(note => note.Details).HasMaxLength(250);
        }
    }
}
