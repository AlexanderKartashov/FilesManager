using System;
using Model.Core;
using Model.Processing;
using System.IO;
using System.Collections.Generic;

namespace FileListPrinter
{
	class Program
	{
		class FileHandler : IDisposable
		{
			private TextWriter _sw;

			public FileHandler(TextWriter sw)
			{
				_sw = sw;
			}

			public void Dispose()
			{
				_sw?.Close();
			}
		}

		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Command line options : <root path> [<output file path>]");
				return;
			}

			var rootPath = args[0];

			var stream = Console.Out;
			if (args.Length == 2)
			{
				stream = System.IO.File.CreateText(args[1]);
			}

			Console.WriteLine("Select enumeration type:");
			Console.WriteLine("1 - in depth");
			Console.WriteLine("2 - files first");
			var strategy = Selector(new Dictionary<int, Func<IEnumerationStrategy>>() {
				{ 1, () => new EnumerateInDepth() },
				{ 2, () => new EnumerateFilesFirst() }
			});

			Console.WriteLine("Select printer type:");
			Console.WriteLine("1 - hierarchy");
			Console.WriteLine("2 - files list");
			var processorStrategy = Selector(new Dictionary<int, Func<Tuple<IItemProcessor, IList<IItemFilter>>>>() {
				{ 1, () => Tuple.Create<IItemProcessor, IList<IItemFilter>>(new HierarchyPrinter(stream), null) },
				{ 2, () => Tuple.Create<IItemProcessor, IList<IItemFilter>>(new HierarchyPrinter(stream, false), new List<IItemFilter>(){ new FilesFilter() }) }
			});

			var modelCreator = new ModelCreator();
			IFileSystemItem model = null;
			try
			{
				model = modelCreator.GetFileSystemIerarchy(rootPath);
			}
			catch (Exception e)
			{
				Console.WriteLine(String.Format("error occured while model creation: {0}", e.Message));
				return;
			}

			try
			{
				var processor = new ItemsProcessor();
				processor.AddProcessorStrategy(processorStrategy.Item1, processorStrategy.Item2);
				processor.Process(model, strategy);
			}
			catch (Exception e)
			{
				Console.WriteLine(String.Format("error occured while model processing: {0}", e.Message));
				return;
			}
			finally
			{
				stream.Flush();
				stream.Close();
			}
		}

		private static T Selector<T>(Dictionary<int, Func<T>> dictionary)
		{
			do
			{
				var choice = Console.ReadLine();
				int parsedChoice = -1;
				if (!int.TryParse(choice, out parsedChoice))
				{
					Console.WriteLine("Invalid input. Enter correct choice.");
					continue;
				}

				if (!dictionary.ContainsKey(parsedChoice))
				{
					Console.WriteLine("Invalid choice. Try again.");
					continue;
				}

				return dictionary[parsedChoice].Invoke();

			} while (true);
		}
	}
}
