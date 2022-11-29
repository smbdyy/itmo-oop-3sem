using Backups.Visitors;

namespace Backups.Repositories;

public class MemoryFileSystemFile : IMemoryFileSystemFile
{
    private byte[] _data;

    public MemoryFileSystemFile(string path, int bufferSize)
    {
        if (bufferSize < 0)
        {
            throw new NotImplementedException();
        }

        Path = path;
        _data = new byte[bufferSize];
    }

    public string Path { get; }

    public void Accept(IMemoryFileSystemVisitor visitor) => visitor.Visit(this);

    public Stream OpenRead()
    {
        return new MemoryStream(_data, false);
    }

    public Stream OpenWrite()
    {
        return new MemoryStream(_data, true);
    }
}