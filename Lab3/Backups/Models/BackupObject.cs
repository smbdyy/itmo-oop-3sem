namespace Backups.Models;
using Zio;

public class BackupObject
{
    public BackupObject(UPath relativePath)
    {
        RelativePath = relativePath;
    }

    public UPath RelativePath { get; }
}