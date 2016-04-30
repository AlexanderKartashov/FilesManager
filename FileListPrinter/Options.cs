using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;

namespace FileListPrinter
{
	class Options
	{
		[Option('o', "output", Required = false, HelpText = "Output file path. If not set, results print to standard output")]
		public String OutputFile { get; set; }

		[Option('r', "root", Required = true, HelpText = "Root folder")]
		public String RootFolder { get; set; }

		[OptionList('t', "tasks", Separator = ':', Required = true, HelpText = "Tasks. Valid values are 's'(collect statistics), 'h'(collect files hierarchy), 'f'(collect files list)")]
		public IList<String> Tasks { get; set; }

		[Option('m', "mode", Required = true, HelpText = "Walkthrought mode. Valid values are 'd' (depth, folders first) or 'f' (depth, files first)")]
		public String Mode { get; set; }

		[Option('p', "print", Required = true, HelpText = "Print result mode. Valid values are 'd' (detailed) or 's' (simple)")]
		public String Printer { get; set; }

		[Option('s', "silent", Required = false, HelpText = "Do not report progress")]
		public bool Silnet { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
		}
	}
}
