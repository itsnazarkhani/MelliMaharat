using MelliMaharat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Score)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.HasOne(x => x.Enrollment)
                .WithMany(x => x.Grades)
                .HasForeignKey(x => x.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Assessment)
                .WithMany(x => x.Grades)
                .HasForeignKey(x => x.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.EnrollmentId, x.AssessmentId })
                .IsUnique();
        }
    }
}
