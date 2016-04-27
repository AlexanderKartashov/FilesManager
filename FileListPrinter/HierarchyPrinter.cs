using Model.Processing;
using System;
using System.Text;
using Model.Core;
using System.IO;

namespace FileListPrinter
{
	class HierarchyPrinter : IItemProcessor
	{
		private TextWriter _tw;
		private bool _addIndent;

		public HierarchyPrinter(TextWriter tw, bool addIndent = true)
		{
			_tw = tw;
			_addIndent = addIndent;
		}

		public void ProcessItem(IFileSystemItem item, int level)
		{
			_tw.WriteLine(Printer(item, level));
		}

		private String Printer(IFileSystemItem item, int level)
		{
			var builder = new StringBuilder();
			if (_addIndent)
			{
				builder.Append('\t', level);
			}
			builder.Append(item.Info.FullName);
			return builder.ToString();
		}
	}
}
