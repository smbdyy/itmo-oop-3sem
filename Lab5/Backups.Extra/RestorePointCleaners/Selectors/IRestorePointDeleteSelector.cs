using Backups.RestorePoints;

namespace Backups.Extra.RestorePointCleaners.Selectors;

public interface IRestorePointDeleteSelector
{
    public IEnumerable<IRestorePoint> GetRestorePointsToDelete(IEnumerable<IRestorePoint> restorePoints);
}