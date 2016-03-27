using System;
using System.Collections.Generic;

namespace Model
{
    public class File : IFileSystemItem
    {
        public File(String name, int size)
        {
            Name = name;
            Size = size;
        }

        public String Name { get; }
        public int Size { get; }
        public IEnumerable<IFileSystemItem> Objects { get { return null; } }
    }
}
