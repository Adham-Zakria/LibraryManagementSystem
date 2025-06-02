using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired();

            builder.Property(b => b.Genre)
                .IsRequired();

            builder.Property(e => e.Genre).HasConversion(
                ValueToAddInDb => ValueToAddInDb.ToString(),    // convert to string when adding to database
                ValueToReadFromDb => (Genre)Enum.Parse(typeof(Genre), ValueToReadFromDb)); // convert to enum again when reading from database

            builder.Property(b => b.Description)
                .HasMaxLength(300);

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    
    }
}
