using Alphaleonis.Win32.Filesystem;
using Model.Core;
using System;
using System.Text;

namespace FileListPrinter
{
	class ExtraInfoPrinter : IPrinter
	{
		public String Print(IFileSystemItem item, int level, bool addIndent)
		{
			var builder = new StringBuilder();
			var info = item.Info;

			builder.Append(String.Format("{0}{1}\n", AddIndent(level, addIndent), info.FullName));
			builder.Append(String.Format("{0}-----------------------------\n", AddIndent(level, addIndent)));
			builder.Append(String.Format("{0}Creation time {1}\n", AddIndent(level, addIndent), info.CreationTime));
			builder.Append(String.Format("{0}Last write time {1}", AddIndent(level, addIndent), info.LastWriteTime));

			var fileInfo = info as FileInfo;
			if (fileInfo != null)
			{
				builder.Append(String.Format("\n{0}File size is {1} bytes", AddIndent(level, addIndent), fileInfo.Length));
			}
			builder.Append(String.Format("\n{0}-----------------------------", AddIndent(level, addIndent)));

			return builder.ToString();
		}

		private String AddIndent(int level, bool addIndent)
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
