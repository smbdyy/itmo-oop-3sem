using Backups.Extra.Interfaces;
using Backups.Models;
using Backups.RestorePoints;

namespace Backups.Extra.Models;

public class DateRestorePointDeleteCreator : IRestorePointDeleteSelector
{
    private readonly DateTime _notOlderThan;

    public DateRestorePointDeleteCreator(DateTime notOlderThan)
    {
        _notOlderThan = notOlderThan;
    }

    public IEnumerable<IRestorePoint> GetRestorePointsToDelete(IEnumerable<IRestorePoint> restorePoints)
    {
        return restorePoints.Where(r => r.CreationDateTime < _notOlderThan);
    }
}