using System;
using System.Collections.Generic;

namespace Model
{
    public interface IFileSystemItem
    {
        String Name { get; }
        int Size { get; }
        IEnumerable<IFileSystemItem> Objects { get; }
    }
}
