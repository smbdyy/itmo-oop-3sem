namespace Backups.Tools.Utils;

public static class MemoryFileSystemUtils
{
    public static IEnumerable<string> SplitPath(string path)
    {
        return path.Split(Path.DirectorySeparatorChar);
    }

    public static string CombineFromArray(IEnumerable<string> splitPath)
    {
        string[] splitPathAsArray = splitPath.ToArray();
        if (!splitPathAsArray.Any())
        {
            throw new NotImplementedException();
        }

        string result = splitPathAsArray[0];
        for (int i = 1; i < splitPathAsArray.Length; i++)
        {
            result = Path.Combine(result, splitPathAsArray[i]);
        }

        return result;
    }
}