using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public interface IFileSystemItem
    {
        FileSystemInfo Info { get; }

        IEnumerable<IFileSystemItem> Objects { get; }
    }
}
