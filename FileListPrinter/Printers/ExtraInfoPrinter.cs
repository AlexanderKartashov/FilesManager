using Alphaleonis.Win32.Filesystem;
using Model.Core;
using System;
using System.Text;

namespace FileListPrinter
{
	class ExtraInfoPrinter : BasePrinter
	{
		public override String Print(IFileSystemItem item, int level, bool addIndent)
		{
			var builder = new StringBuilder();
			var info = item.Info;

			builder.Append(String.Format("{0}{1}\n", AddIndent(level, addIndent), info.FullName));
			builder.Append(String.Format("{0}{1}\n", AddIndent(level, addIndent), Delimeter()));
			builder.Append(String.Format("{0}Creation time {1}\n", AddIndent(level, addIndent), info.CreationTime));
			builder.Append(String.Format("{0}Last write time {1}\n", AddIndent(level, addIndent), info.LastWriteTime));

			var fileInfo = info as FileInfo;
			if (fileInfo != null)
			{
				builder.Append(String.Format("{0}File size is {1} bytes\n", AddIndent(level, addIndent), fileInfo.Length));
			}
			builder.Append(String.Format("{0}{1}\n", AddIndent(level, addIndent), Delimeter()));

			return builder.ToString();
		}

		private String Delimeter()
		{
			var sb = new StringBuilder();
			sb.Append('-', 40);
			return sb.ToString();
		}
	}
}
