using Model.Processing;
using System.IO;

namespace FileListPrinter
{
	interface IItemProcessorWithOutput : IItemProcessor
	{
		void PrintResults(TextWriter tw);
	}
}
