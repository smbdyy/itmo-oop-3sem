using Backups.Extra.Interfaces;
using Backups.Models;

namespace Backups.Extra.Models;

public class EveryLimitRestorePointDeleteSelector : IRestorePointDeleteSelector
{
    private List<IRestorePointDeleteSelector> _selectors;

    public EveryLimitRestorePointDeleteSelector(IEnumerable<IRestorePointDeleteSelector> selectors)
    {
        _selectors = selectors.ToList();
        if (_selectors.Count == 0)
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerable<IRestorePoint> GetRestorePointsToDelete(IEnumerable<IRestorePoint> restorePoints)
    {
        var restorePointsAsList = restorePoints.ToList();
        IEnumerable<IRestorePoint> restorePointsToDelete = _selectors[0].GetRestorePointsToDelete(restorePointsAsList);
        for (int i = 1; i < _selectors.Count; i++)
        {
            IEnumerable<IRestorePoint> currentSelectorResult = _selectors[i].GetRestorePointsToDelete(restorePointsAsList);
            restorePointsToDelete = restorePointsToDelete.Intersect(currentSelectorResult);
        }

        return restorePointsToDelete;
    }
}