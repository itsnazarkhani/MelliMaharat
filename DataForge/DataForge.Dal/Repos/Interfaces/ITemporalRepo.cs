namespace DataForge.Dal.Repos.Interfaces;

public interface ITemporalRepo<T> : IRepo<T> where T : BaseEntity, new()
{
    IEnumerable<TemporalView<T>> GetAllHistory();
    IEnumerable<TemporalView<T>> GetHistoryAsOf(DateTime dateTime);
    IEnumerable<TemporalView<T>> GetHistoryBetween(DateTime startDateTime, DateTime endDateTime);
    IEnumerable<TemporalView<T>> GetHistoryContainedIn(DateTime startDateTime, DateTime endDateTime);
    IEnumerable<TemporalView<T>> GetHistoryFromTo(DateTime startDateTime, DateTime endDateTime);
}