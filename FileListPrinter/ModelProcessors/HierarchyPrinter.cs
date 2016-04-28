using Model.Core;
using System.IO;
using System;
using System.Text;

namespace FileListPrinter
{
	class HierarchyPrinter : BaseItemProcessorWithOutput
	{
		private bool _addIndent;
		private IPrinter _printer;

		public HierarchyPrinter(IPrinter printer, bool addIndent = true)
			: base()
		{
			_printer = printer;
			_addIndent = addIndent;
		}

		public override void ProcessItem(IFileSystemItem item, int level)
		{
			_sb.Append(_printer.Print(item, level, _addIndent));
		}

		protected override String PrologContent()
		{
			return _addIndent ? "\tHierarchy" : "\tFiles list";
		}
	}
}
