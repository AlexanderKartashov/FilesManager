﻿using Model.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core;
using System.IO;

namespace FileListPrinter
{
	class HierarchyPrinter : IItemProcessor
	{
		private TextWriter _tw;

		public HierarchyPrinter(TextWriter tw)
		{
			_tw = tw;
		}

		public void ProcessItem(IFileSystemItem item, int level)
		{
			_tw.WriteLine(Printer(item, level));
		}

		private String Printer(IFileSystemItem item, int level)
		{
			var builder = new StringBuilder();
			builder.Append('=', level);
			builder.Append(' ');
			builder.Append(item.Info.FullName);
			return builder.ToString();
		}
	}
}