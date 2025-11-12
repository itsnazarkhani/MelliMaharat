namespace DataForge.Models.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder
            .OwnsOne
            (
                x => x.PersonInformation,
                y =>
                {
                    y.Property(x => x.FirstName)
                    .HasColumnName(nameof(Person.FirstName))
                    .HasColumnType("nvarchar(50)");

                    y.Property(x => x.LastName)
                    .HasColumnName(nameof(Person.LastName))
                    .HasColumnType("nvarchar(50)");

                    y.Property(x => x.Email)
                    .HasColumnName(nameof(Person.Email))
                    .HasColumnType("nvarchar(50)");

                    y.Property(x => x.Age)
                    .HasColumnName(nameof(Person.Age))
                    .HasColumnType("int");

                    y.Property(x => x.NationalCode)
                    .HasColumnName(nameof(Person.NationalCode))
                    .HasMaxLength(20);

                    y.Property(x => x.PhoneNumber)
                    .HasColumnName(nameof(Person.PhoneNumber))
                    .HasMaxLength(20);

                    y.Property(x => x.FullName)
                    .HasColumnName(nameof(Person.FullName))
                    .HasComputedColumnSql("[FirstName] + ', ' + [LastName]", stored: true)
                    .HasColumnType("nvarchar(50)");
                }
            );


        builder
            .Navigation(x => x.PersonInformation)
            .IsRequired(true);
    }
}