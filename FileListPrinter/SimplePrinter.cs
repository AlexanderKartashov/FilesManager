using System;
using System.Text;
using Model.Core;

namespace FileListPrinter
{
	class SimplePrinter : IPrinter
	{
		public String Print(IFileSystemItem item, int level, bool addIndent)
		{
			var builder = new StringBuilder();
			if (addIndent)
			{
				builder.Append('\t', level);
			}
			builder.Append(item.Info.FullName);
			return builder.ToString();
		}
	}
}
