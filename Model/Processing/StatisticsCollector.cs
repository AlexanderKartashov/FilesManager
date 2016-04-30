using System;
using System.Collections.Generic;
using Model.Core;
using Alphaleonis.Win32.Filesystem;

namespace Model
{
	namespace Processing
	{
		public class StatisticsCollector : IItemProcessor
		{
			public long TotalSize { get; set; } = 0;
			public long FilesCount { get; set; } = 0;
			public ISet<String> Extensions { get; set; } = new SortedSet<String>();

			public void ProcessItem(IFileSystemItem item, int level)
			{
				var fileInfo = item.Info as FileInfo;
				if (fileInfo != null)
				{
					Extensions.Add(fileInfo.Extension);

					TotalSize += fileInfo.Length;
					++FilesCount;
				}
			}
		}
	}
}