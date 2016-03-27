using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public interface IFileSystemItem
    {
        String Path { get; }
        int Size { get; }

        DateTime CreationDate { get; }
        DateTime ModificationDate { get; }

        IEnumerable<IFileSystemItem> Objects { get; }
    }
}
