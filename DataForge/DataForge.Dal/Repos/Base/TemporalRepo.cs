namespace DataForge.Dal.Repos.Base
{
    public class TemporalRepo<T> : Repo<T>, ITemporalRepo<T> where T : BaseEntity, new()
    {
        public TemporalRepo() : base() { }
        public TemporalRepo(ApplicationDbContext context) : base(context)
        {
        }

        public virtual IEnumerable<TemporalView<T>> GetAllHistory()
        {
            IQueryable<T> query = _table.TemporalAll();
            return ExecuteQuery(query);
        }

        public virtual IEnumerable<TemporalView<T>> GetHistoryAsOf(DateTime dateTime)
        {
            DateTime utcDateTime = ConvertTimeToUtc(dateTime, Local);
            IQueryable<T> query = _table.TemporalAsOf(utcDateTime);
            return ExecuteQuery(query);
        }

        public virtual IEnumerable<TemporalView<T>> GetHistoryBetween(DateTime startDateTime, DateTime endDateTime)
        {
            DateTime utcStartDateTime = ConvertTimeToUtc(startDateTime, Local);
            DateTime utcEndDateTime = ConvertTimeToUtc(endDateTime, Local);
            IQueryable<T> query = _table.TemporalBetween(utcStartDateTime, utcEndDateTime);
            return ExecuteQuery(query);
        }

        public virtual IEnumerable<TemporalView<T>> GetHistoryContainedIn(DateTime startDateTime, DateTime endDateTime)
        {
            DateTime utcStartDateTime = ConvertTimeToUtc(startDateTime, Local);
            DateTime utcEndDateTime = ConvertTimeToUtc(endDateTime, Local);
            IQueryable<T> query = _table.TemporalContainedIn(utcStartDateTime, utcEndDateTime);
            return ExecuteQuery(query);
        }

        public virtual IEnumerable<TemporalView<T>> GetHistoryFromTo(DateTime startDateTime, DateTime endDateTime)
        {
            DateTime utcStartDateTime = ConvertTimeToUtc(startDateTime, Local);
            DateTime utcEndDateTime = ConvertTimeToUtc(endDateTime, Local);
            IQueryable<T> query = _table.TemporalFromTo(utcStartDateTime, utcEndDateTime);
            return ExecuteQuery(query);
        }
        public IEnumerable<TemporalView<T>> ExecuteQuery(IQueryable<T> query)
        {
            return query
                    .OrderBy(x => EF.Property<DateTime>(x, "ValidFrom"))
                    .Select
                    (
                        x => new TemporalView<T>()
                        {
                            Entity = x,
                            ValidFrom = EF.Property<DateTime>(x, "ValidFrom"),
                            ValidTo = EF.Property<DateTime>(x, "ValidTo")
                        }
                    );
        }
    }
}
