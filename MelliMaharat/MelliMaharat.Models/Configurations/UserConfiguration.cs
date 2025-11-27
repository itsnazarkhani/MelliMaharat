using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(x => x.Username)
                .IsUnique(true);

            builder
               .OwnsOne
               (
                   x => x.PersonInformation,
                   y =>
                   {
                       y.Property(x => x.FirstName)
                            .HasColumnType("nvarchar(50)");

                       y.Property(x => x.LastName)
                            .HasColumnType("nvarchar(50)");
                   }
               );
        }
    }
}
