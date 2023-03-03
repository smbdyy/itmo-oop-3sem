using Backups.RestorePoints;

namespace Backups.Extra.RestorePointCleaners.Selectors;

public class AnyLimitRestorePointDeleteSelector : IRestorePointDeleteSelector
{
    private readonly List<IRestorePointDeleteSelector> _selectors;

    public AnyLimitRestorePointDeleteSelector(IEnumerable<IRestorePointDeleteSelector> selectors)
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