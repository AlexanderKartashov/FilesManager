using Alphaleonis.Win32.Filesystem;
using System.Collections.Generic;

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
