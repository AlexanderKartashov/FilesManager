using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	class FileSystemEnumerator
	{
		public void Enumerate(IFileSystemItem root, IItemsProcessor itemsProcessor)
		{
			EnumerateRecursive(root, itemsProcessor, 0);
		}

		private void EnumerateRecursive(IFileSystemItem root, IItemsProcessor itemsProcessor, int level)
		{
			itemsProcessor.Process(root, level);
			root.Objects?.ToList().ForEach((IFileSystemItem item) => EnumerateRecursive(item, itemsProcessor, level + 1));
		}
	}
}
