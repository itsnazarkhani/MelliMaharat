namespace DataForge.Dal.DbContexts;

public class ApplicationDbContext : DbContext
{
    #region Tables
    public virtual DbSet<Master> Masters { get; set; }
    public virtual DbSet<Lesson> Lessons { get; set; }
    public virtual DbSet<Presentation> Presentations { get; set; }
    public virtual DbSet<LessonInformationView> LessonInformationViews { get; set; }
    #endregion

    #region Constructors
    public ApplicationDbContext(): base() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    #endregion

    #region ApplyConfigurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MasterConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new PresentationConfiguration());
        modelBuilder.ApplyConfiguration(new LessonInformationViewConfiguration());
    }
    #endregion

    #region Conventions
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.IgnoreAny<INonPersisted>();
        configurationBuilder.Properties<string>().HaveMaxLength(50);
    }
    #endregion
}