using MelliMaharat.Domain.Common;
using MelliMaharat.Domain.Entities;
using MelliMaharat.Infrastructure.Identity;
using MelliMaharat.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly AuditableEntityInterceptor _auditableInterceptor;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            AuditableEntityInterceptor auditableInterceptor)
            : base(options)
        {
            _auditableInterceptor = auditableInterceptor;
        }

        // Academic core
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseOffering> CourseOfferings => Set<CourseOffering>();

        // Enrollment
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        // Grades
        public DbSet<Assessment> Assessments => Set<Assessment>();
        public DbSet<Grade> Grades => Set<Grade>();

        // Attendance
        public DbSet<CourseSession> CourseSessions => Set<CourseSession>();
        public DbSet<Attendance> Attendances => Set<Attendance>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.RowVersion))
                        .IsRowVersion()
                        .IsConcurrencyToken();
                }
            }

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
