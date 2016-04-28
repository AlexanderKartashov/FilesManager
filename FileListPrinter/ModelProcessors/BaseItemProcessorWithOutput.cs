using System.Text;
using Model.Core;
using System.IO;
using System;

namespace FileListPrinter
{
	abstract class BaseItemProcessorWithOutput : IItemProcessorWithOutput
	{
		protected StringBuilder _sb = new StringBuilder();

		public BaseItemProcessorWithOutput()
		{
			Prolog();
		}

		public virtual void PrintResults(TextWriter tw)
		{
			tw.Write(_sb.ToString());
		}

		public abstract void ProcessItem(IFileSystemItem item, int level);

		private void Prolog()
		{
			_sb.Append('=', 40);
			_sb.Append('\n');
			_sb.Append(PrologContent());
			_sb.Append('\n');
			_sb.Append('=', 40);
			_sb.Append('\n');
		}

		protected abstract String PrologContent();
	}
}
