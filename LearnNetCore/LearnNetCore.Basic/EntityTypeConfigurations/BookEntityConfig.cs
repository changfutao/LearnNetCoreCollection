using LearnNetCore.Basic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnNetCore.Basic.EntityTypeConfigurations
{
    public class BookEntityConfig : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("T_Book");
            builder.Property(a => a.Title).HasMaxLength(50);
            builder.Property(a => a.AuthorName).HasMaxLength(20);
        }
    }
}
