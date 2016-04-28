using Model.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileListPrinter
{
	class StatisticsCollector : BaseItemProcessorWithOutput
	{
		private long _totalSize = 0;
		private long _filesCount = 0;
		private ISet<String> _extensions = new SortedSet<String>();

		public override void ProcessItem(IFileSystemItem item, int level)
		{
			var fileInfo = item.Info as Alphaleonis.Win32.Filesystem.FileInfo;
			if (fileInfo != null)
			{
				_extensions.Add(fileInfo.Extension);
				_totalSize += fileInfo.Length;
				++_filesCount;
			}
		}

		public override void PrintResults(TextWriter tw)
		{
			_sb.Append(String.Format("Files count = {0}\n", _filesCount));
			_sb.Append(String.Format("Files total size = {0}\n", _totalSize));
			_sb.Append(String.Format("File types: {0}\n", _extensions.Aggregate((String text, String item) => text + ", " + item)));
			base.PrintResults(tw);
		}

		protected override String PrologContent()
		{
			return "\tStatistics";
		}
	}
}
