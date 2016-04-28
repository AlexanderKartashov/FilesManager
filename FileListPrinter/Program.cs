using System;
using System.Linq;
using System.Collections.Generic;
using CommandLine;
using Model.Processing;
using Model.Core;

namespace FileListPrinter
{
	class Program
	{
		static void Main(string[] args)
		{
			var options = new Options();
			var parser = new Parser((settings) => {
				settings.CaseSensitive = false;
				settings.MutuallyExclusive = true;
				settings.HelpWriter = Console.Error;
			});

			bool success = parser.ParseArguments(args, options);
			if (!success)
			{
				return;
			}

			IEnumerationStrategy strategy = null;
			IPrinter printer = null;

			var processor = new ItemsProcessor();
			var processorStrategiesList = new List<IItemProcessorWithOutput>();

			try
			{
				strategy = Selector(options.Mode, "Invalid walkthrought mode", new Dictionary<String, Func<IEnumerationStrategy>>() {
					{ "d", () => new EnumerateInDepth() },
					{ "f", () => new EnumerateFilesFirst() }
				});

				printer = Selector(options.Printer, "Invalid print mode", new Dictionary<string, Func<IPrinter>>() {
					{ "s" , () => new SimplePrinter() },
					{ "d" , () => new ExtraInfoPrinter() }
				});

				options.Tasks.Distinct().ToList().ForEach((String item) =>
				{
					var tuple = Selector(item, "Invalid task", new Dictionary<String, Func<Tuple<IItemProcessorWithOutput, IList<IItemFilter>>>>() {
						{ "h", () => Tuple.Create<IItemProcessorWithOutput, IList<IItemFilter>>(new HierarchyPrinter(printer), null) },
						{ "f", () => Tuple.Create<IItemProcessorWithOutput, IList<IItemFilter>>(new HierarchyPrinter(printer, false), new List<IItemFilter>(){ new FilesFilter() }) },
						{ "s", () => Tuple.Create<IItemProcessorWithOutput, IList<IItemFilter>>(new StatisticsCollector(), new List<IItemFilter>(){ new FilesFilter() }) }
					});

					processorStrategiesList.Add(tuple.Item1);
					processor.AddProcessorStrategy(tuple.Item1, tuple.Item2);
				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			var stream = Console.Out;
			if (options.OutputFile != null)
			{
				stream = System.IO.File.CreateText(options.OutputFile);
			}

			Console.WriteLine("Creating model");

			var modelCreator = new ModelCreator();
			IFileSystemItem model = null;
			try
			{
				model = modelCreator.GetFileSystemIerarchy(options.RootFolder);
			}
			catch (Exception e)
			{
				Console.WriteLine(String.Format("error occured while model creation: {0}", e.Message));
				return;
			}

			Console.WriteLine("Processing model");

			try
			{
				processor.Process(model, strategy);
				processorStrategiesList.ForEach((IItemProcessorWithOutput item) => item.PrintResults(stream));
			}
			catch (Exception e)
			{
				Console.WriteLine(String.Format("error occured while model processing: {0}", e.Message));
			}
			finally
			{
				stream.Flush();
				stream.Close();
			}
		}

		private static T Selector<T>(String key, String errorDescription, Dictionary<String, Func<T>> creatorMap)
		{
			if (!creatorMap.ContainsKey(key))
			{
				throw new ArgumentException(String.Format("{0} {1}", errorDescription, key));
			}

			return creatorMap[key]();
		}
	}
}
