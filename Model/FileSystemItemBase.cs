using System;
using System.Collections.Generic;

namespace Model
{
    public abstract class FileSystemItemBase : IFileSystemItem
    {
        public FileSystemItemBase(String path, int size, DateTime creationDate, DateTime modificationDate)
        {
            Path  = path;
            Size = size;
            CreationDate = creationDate;
            ModificationDate = modificationDate;
        }

        public FileSystemItemBase()
        {
        }

        public String Path { get; }
        public int Size { get; }

        public DateTime CreationDate { get; }
        public DateTime ModificationDate { get; }

        public abstract IEnumerable<IFileSystemItem> Objects { get; }
    }
}
