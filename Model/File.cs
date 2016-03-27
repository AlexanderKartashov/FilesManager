using System;
using System.Collections.Generic;

namespace Model
{
    public class File : FileSystemItemBase
    {
        public File(String path, int size, DateTime creationDate, DateTime modificationDate)
            : base(path, size, creationDate, modificationDate)
        {
        }

        public override IEnumerable<IFileSystemItem> Objects { get { return null; } }
    }
}
