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
		private IPrinter _printer;

		public HierarchyPrinter(TextWriter tw, IPrinter printer, bool addIndent = true)
		{
			_printer = printer;
			_tw = tw;
			_addIndent = addIndent;
		}

		public void ProcessItem(IFileSystemItem item, int level)
		{
			_tw.WriteLine(_printer.Print(item, level, _addIndent));
		}
	}
}
