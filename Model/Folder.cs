using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Folder : IFileSystemItem
    {
        private List<IFileSystemItem> _items = new List<IFileSystemItem>();

        public Folder(String name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; }
        public int Size { get; }
        public IEnumerable<IFileSystemItem> Objects { get { return _items; } }

        public void Add(IFileSystemItem item)
        {
            _items.Add(item);
        }
    }
}
