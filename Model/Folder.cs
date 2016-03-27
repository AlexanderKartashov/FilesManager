using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Folder : FileSystemItemBase
    {
        private List<IFileSystemItem> _items = new List<IFileSystemItem>();

        public Folder(String path, int size, DateTime creationDate, DateTime modificationDate)
            : base(path, size, creationDate, modificationDate)
        {
        }

        public override IEnumerable<IFileSystemItem> Objects { get { return _items; } }

        public void Add(IFileSystemItem item)
        {
            _items.Add(item);
        }
    }
}
