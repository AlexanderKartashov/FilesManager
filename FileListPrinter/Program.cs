using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core;
using Model.Processing;

namespace FileListPrinter
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				return;
			}

			var path = args[0];

			var modelCreator = new ModelCreator();
			IFileSystemItem model = null;
			try
			{
				model = modelCreator.GetFileSystemIerarchy(path);
			}
			catch (Exception)
			{
				return;
				//throw;
			}

			var processor = new ItemsProcessor();
			try
			{
				processor.AddProcessorStrategy(new HierarchyPrinter(Console.Out));
				processor.Process(model, new EnumerateInDepth());
			}
			catch(Exception)
			{
				return;
			}
		}
	}
}
