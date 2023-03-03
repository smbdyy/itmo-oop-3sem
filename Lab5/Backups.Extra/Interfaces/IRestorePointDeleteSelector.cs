using Backups.Models;
using Backups.RestorePoints;

namespace Backups.Extra.Interfaces;

public interface IRestorePointDeleteSelector
{
    public IEnumerable<IRestorePoint> GetRestorePointsToDelete(IEnumerable<IRestorePoint> restorePoints);
}