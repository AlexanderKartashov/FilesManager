using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public abstract class FileSystemItemBase : IFileSystemItem
    {
        public FileSystemItemBase(String path)
        {
            Info = CreateInfo(path);
        }

        public FileSystemInfo Info { get; }

        public abstract IEnumerable<IFileSystemItem> Objects { get; }

        protected abstract FileSystemInfo CreateInfo(String path);
    }
}
