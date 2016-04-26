using System.Collections.Generic;
using System.IO;

namespace Model
{
	namespace Core
	{
		public interface IFileSystemItem
		{
			FileSystemInfo Info { get; }

			IEnumerable<IFileSystemItem> Objects { get; }
		}
	}
}
