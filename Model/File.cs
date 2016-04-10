using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class File : FileSystemItemBase
    {
        public File(String path)
            : base(path)
        {
        }

        public override IEnumerable<IFileSystemItem> Objects { get { return null; } }

        protected override FileSystemInfo CreateInfo(string path)
        {
            return new FileInfo(path);
        }
    }
}
