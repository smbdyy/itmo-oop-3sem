using Backups.Extra.Interfaces;
using Backups.Models;
using Backups.RestorePoints;

namespace Backups.Extra.Models;

public class AmountRestorePointDeleteSelector : IRestorePointDeleteSelector
{
    private readonly int _amount;

    public AmountRestorePointDeleteSelector(int amount)
    {
        if (amount < 0)
        {
            throw new NotImplementedException();
        }

        _amount = amount;
    }

    public IEnumerable<IRestorePoint> GetRestorePointsToDelete(IEnumerable<IRestorePoint> restorePoints)
    {
        var restorePointsAsList = restorePoints.ToList();
        int amount = restorePointsAsList.Count < _amount ? 0 : restorePointsAsList.Count - _amount;
        return restorePointsAsList.GetRange(0, amount);
    }
}