using MelliMaharat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class CourseOfferingConfiguration : IEntityTypeConfiguration<CourseOffering>
    {
        public void Configure(EntityTypeBuilder<CourseOffering> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Semester)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Capacity)
                .IsRequired();

            builder.HasOne(x => x.Course)
                .WithMany(x => x.Offerings)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Instructor)
                .WithMany()
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
