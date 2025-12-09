namespace MelliMaharat.Dal.DbContexts;

public class ApplicationDbContext : DbContext
{
    #region Tables
    public virtual DbSet<Master> Masters { get; set; }
    public virtual DbSet<Lesson> Lessons { get; set; }
    public virtual DbSet<Presentation> Presentations { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Selection> Selections { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Session> Sessions { get; set; }
    public virtual DbSet<Attendance> Attendances { get; set; }
    public virtual DbSet<Term> Terms { get; set; }
    public virtual DbSet<SelectionTime> SelectionTimes { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<SelectionFeedback> SelectionFeedbacks { get; set; }
    public virtual DbSet<LessonInformationView> LessonInformationViews { get; set; }
    public virtual DbSet<SelectedLessonsInformationView> SelectedLessonsInformationViews { get; set; }
    #endregion

    #region Constructors
    public ApplicationDbContext(): base() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    #endregion

    #region ApplyConfigurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    #endregion

    #region Conventions
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.IgnoreAny<INonPersisted>();
        configurationBuilder.Properties<string>().HaveMaxLength(50);
    }
    #endregion

    #region CustomMethods
    public bool Create() => Database.EnsureCreated();
    public void Migrate() => Database.Migrate();
    public bool Delete() => Database.EnsureDeleted();
    public static void FactoryMigrate() => new ApplicationDbContextFactory().CreateDbContext().Migrate();
    public static bool FactoryDelete() => new ApplicationDbContextFactory().CreateDbContext().Delete();
    public static bool FactoryCreate() => new ApplicationDbContextFactory().CreateDbContext().Create();
    #endregion
}