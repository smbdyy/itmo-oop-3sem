namespace Backups.Models;
using Zio;

public class BackupObject
{
    public BackupObject(UPath path)
    {
        ObjectPath = path;
    }

    public UPath ObjectPath { get; }
}