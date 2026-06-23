using MelliMaharat.Domain.Entities;
using MelliMaharat.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StudentNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(x => x.StudentNumber)
                .IsUnique();

            builder.HasOne(x => x.Department)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Student -> ApplicationUser
            builder.HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Student>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
