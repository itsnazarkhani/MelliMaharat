using MelliMaharat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.EnrolledAt)
                .IsRequired();

            builder.HasOne(x => x.Student)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CourseOffering)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.CourseOfferingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate enrollment
            builder.HasIndex(x => new { x.StudentId, x.CourseOfferingId })
                .IsUnique();
        }
    }
}
