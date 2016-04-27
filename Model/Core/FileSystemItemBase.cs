using Alphaleonis.Win32.Filesystem;
using System;
using System.Collections.Generic;

namespace Model
{
	namespace Core
	{
		public abstract class FileSystemItemBase : IFileSystemItem
		{
			public FileSystemItemBase(String path)
			{
				Info = CreateInfo(path);
			}

			public FileSystemInfo Info { get; }

			public abstract IEnumerable<IFileSystemItem> Objects { get; }

			protected abstract FileSystemInfo CreateInfo(String path);
		} 
	}
}
