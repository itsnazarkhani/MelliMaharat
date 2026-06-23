using MelliMaharat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Weight)
                .IsRequired();

            builder.Property(x => x.MaxScore)
                .IsRequired();

            builder.HasOne(x => x.CourseOffering)
                .WithMany()
                .HasForeignKey(x => x.CourseOfferingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
