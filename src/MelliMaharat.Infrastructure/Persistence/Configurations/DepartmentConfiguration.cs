using MelliMaharat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Code)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(x => x.Code)
                .IsUnique();

            // Department -> Students
            builder.HasMany(x => x.Students)
                .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Restrict);

            // Department -> Instructors
            builder.HasMany(x => x.Instructors)
                .WithOne()
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.Restrict);

            // Department -> Courses
            builder.HasMany(x => x.Courses)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
