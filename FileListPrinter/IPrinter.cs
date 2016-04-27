using Model.Core;
using System;

namespace FileListPrinter
{
	interface IPrinter
	{
		String Print(IFileSystemItem item, int level, bool addIndent);
	}
}
