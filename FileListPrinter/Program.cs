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

			var dictionary = new Dictionary<int, Func<IEnumerationStrategy>>() {
				{ 1, () => new EnumerateInDepth() },
				{ 2, () => new EnumerateFilesFirst() }
			};

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

				var strategy = dictionary[parsedChoice].Invoke();

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

				var processor = new ItemsProcessor();
				try
				{
					processor.AddProcessorStrategy(new HierarchyPrinter(stream));
					processor.Process(model, strategy);
					break;
				}
				catch (Exception e)
				{
					Console.WriteLine(String.Format("error occured while model processing: {0}", e.Message));
					return;
				}

			} while (true);

			stream.Flush();
			stream.Close();
		}
	}
}
