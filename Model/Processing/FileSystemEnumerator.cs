using Model.Core;
using System;
using System.Linq;

namespace Model
{
	namespace Processing
	{
		public class FileSystemEnumerator
		{
			public delegate void ProcessItemEventHandler(IFileSystemItem item, int level);

			public event ProcessItemEventHandler ProcessItemEvent;

			private IEnumerationStrategy _enumerationStrategy;

			public FileSystemEnumerator(IEnumerationStrategy enumerationStrategy)
			{
				if (enumerationStrategy == null)
				{
					throw new ArgumentNullException("enumeration strategu must be not null");
				}

				_enumerationStrategy = enumerationStrategy;
				_enumerationStrategy.Init((IFileSystemItem item, int level) => ProcessItemEvent?.Invoke(item, level));
			}

			public void Enumerate(IFileSystemItem root)
			{
				if (root == null)
				{
					throw new ArgumentNullException("root item must be not null");
				}

				_enumerationStrategy.Enumerate(root);
			}
		} 
	}
}
