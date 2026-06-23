using MelliMaharat.Domain.Entities;
using MelliMaharat.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmployeeNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(x => x.EmployeeNumber)
                .IsUnique();

            builder.HasOne(x => x.Department)
                .WithMany(x => x.Instructors)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Instructor -> ApplicationUser
            builder.HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Instructor>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
