namespace MelliMaharat.Models.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder
                .HasOne(s => s.Selection)
                .WithMany(sel => sel.Sessions)
                .HasForeignKey(s => s.SelectionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
