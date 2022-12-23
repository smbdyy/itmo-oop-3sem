using Backups.Extra.Interfaces;
using Backups.Models;

namespace Backups.Extra.Models;

public class AnyLimitHybridRestorePointDeleteSelector : IRestorePointDeleteSelector
{
    private readonly List<IRestorePointDeleteSelector> _selectors;

    public AnyLimitHybridRestorePointDeleteSelector(IEnumerable<IRestorePointDeleteSelector> selectors)
    {
        _selectors = selectors.ToList();
        if (_selectors.Count == 0)
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerable<IRestorePoint> GetRestorePointsToDelete(IEnumerable<IRestorePoint> restorePoints)
    {
        var restorePointsToDelete = new List<IRestorePoint>();
        var restorePointsAsList = restorePoints.ToList();
        foreach (IRestorePointDeleteSelector selector in _selectors)
        {
            restorePointsToDelete.AddRange(selector.GetRestorePointsToDelete(restorePointsAsList));
        }

        return restorePointsToDelete;
    }
}