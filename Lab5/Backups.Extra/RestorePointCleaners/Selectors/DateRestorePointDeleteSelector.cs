using Backups.RestorePoints;

namespace Backups.Extra.RestorePointCleaners.Selectors;

public class DateRestorePointDeleteSelector : IRestorePointDeleteSelector
{
    private readonly DateTime _notOlderThan;

    public DateRestorePointDeleteSelector(DateTime notOlderThan)
    {
        _notOlderThan = notOlderThan;
    }

    public IEnumerable<IRestorePoint> GetRestorePointsToDelete(IEnumerable<IRestorePoint> restorePoints)
    {
        return restorePoints.Where(r => r.CreationDateTime < _notOlderThan);
    }
}