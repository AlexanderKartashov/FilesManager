using Model.Core;
using Model.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileListPrinter
{
	class StatisticsCollectorPrinter : BaseItemProcessorWithOutput
	{
		private List<String> _sizes = new List<String>() { "bytes", "KB", "MB", "GB", "TB" };

		private StatisticsCollector _collector = new StatisticsCollector();

		public override void ProcessItem(IFileSystemItem item, int level)
		{
			_collector.ProcessItem(item, level);
		}

		public override void PrintResults(TextWriter tw)
		{
			_sb.Append(String.Format("Files count = {0}\n", _collector.FilesCount));
			_sb.Append(String.Format("Files total size = {0}\n", FormatSizeString()));
			_sb.Append(String.Format("File types: {0}\n", _collector.Extensions.Aggregate((String text, String item) => text + ", " + item)));
			base.PrintResults(tw);
		}

		private String FormatSizeString()
		{
			const int divider = 1024;

			long size = _collector.TotalSize;
			IEnumerator<String> enumerator = _sizes.GetEnumerator();
			enumerator.Reset();

			var sb = new StringBuilder();
			do
			{
				if (!enumerator.MoveNext())
				{
					break;
				}

				sb.Append(String.Format("{0} {1} | ", size, enumerator.Current));
				size /= divider;

			} while (size > 0);

			return sb.ToString();
		}

		protected override String PrologContent()
		{
			return "\tStatistics";
		}
	}
}
