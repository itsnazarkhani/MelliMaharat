namespace DataForge.Dal.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        // Set's
        public virtual DbSet<Master> Masters { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Presentation> Presentations { get; set; }
        public virtual DbSet<LessonInformationView> LessonInformationViews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public void DeleteDatabase()
        {
            this.Database.EnsureDeleted();
        }
        public void MigrateDatabase()
        {
            this.Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MasterConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new PresentationConfiguration());
            modelBuilder.ApplyConfiguration(new LessonInformationViewConfiguration());
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.IgnoreAny<INonPersisted>();
        }
    }
}
