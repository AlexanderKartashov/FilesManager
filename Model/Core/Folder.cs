using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
	namespace Core
	{
		public class Folder : FileSystemItemBase
		{
			private List<IFileSystemItem> _items = new List<IFileSystemItem>();

			public Folder(String path)
				: base(path)
			{
			}

			public override IEnumerable<IFileSystemItem> Objects { get { return _items; } }

			public void Add(IFileSystemItem item)
			{
				_items.Add(item);
			}

			protected override FileSystemInfo CreateInfo(String path)
			{
				return new DirectoryInfo(path);
			}
		} 
	}
}
