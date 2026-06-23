using MelliMaharat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasOne(x => x.CourseSession)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.CourseSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate attendance records
            builder.HasIndex(x => new { x.CourseSessionId, x.StudentId })
                .IsUnique();
        }
    }
}
