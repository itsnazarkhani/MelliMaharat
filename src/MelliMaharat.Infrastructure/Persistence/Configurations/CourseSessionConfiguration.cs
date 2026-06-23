using MelliMaharat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class CourseSessionConfiguration : IEntityTypeConfiguration<CourseSession>
    {
        public void Configure(EntityTypeBuilder<CourseSession> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.HasOne(x => x.CourseOffering)
                .WithMany()
                .HasForeignKey(x => x.CourseOfferingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
