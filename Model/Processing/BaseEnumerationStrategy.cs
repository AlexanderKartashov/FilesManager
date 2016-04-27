using System;
using Model.Core;

namespace Model
{
	namespace Processing
	{
		public abstract class BaseEnumerationStrategy : IEnumerationStrategy
		{
			public void Init(FileSystemEnumerator.ProcessItemEventHandler handler)
			{
				_handler = handler;
			}

			public void Enumerate(IFileSystemItem root)
			{
				if (root == null)
				{
					throw new ArgumentNullException("root item must be not null");
				}

				EnumerateImpl(root, 0);
			}

			protected abstract void EnumerateImpl(IFileSystemItem item, int level);

			protected FileSystemEnumerator.ProcessItemEventHandler _handler;
		}
	}
}