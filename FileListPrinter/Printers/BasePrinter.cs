using System;
using System.Text;
using Model.Core;

namespace FileListPrinter
{
	abstract class BasePrinter : IPrinter
	{
		public abstract String Print(IFileSystemItem item, int level, bool addIndent);

		protected String AddIndent(int level, bool addIndent)
		{
			var builder = new StringBuilder();
			if (addIndent)
			{
				builder.Append('\t', level);
			}
			return builder.ToString();
		}
	}
}
