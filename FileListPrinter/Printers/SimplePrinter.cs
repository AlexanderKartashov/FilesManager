using System;
using System.Text;
using Model.Core;

namespace FileListPrinter
{
	class SimplePrinter : BasePrinter
	{
		public override String Print(IFileSystemItem item, int level, bool addIndent)
		{
			return String.Format("{0}{1}\n", AddIndent(level, addIndent), item.Info.FullName);
		}
	}
}
