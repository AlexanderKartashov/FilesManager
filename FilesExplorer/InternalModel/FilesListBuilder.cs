using Model.Core;
using Model.Processing;
using System;
using System.Collections.ObjectModel;

namespace FilesExplorer
{
	namespace InternalModel
	{
		public class FilesListBuilder : IItemProcessor
		{
			private String _rootFolder;

			public ObservableCollection<FileNode> Files { get; set; } = new ObservableCollection<FileNode>();

			public FilesListBuilder(String rootFolder)
			{
				_rootFolder = rootFolder;
			}

			public void ProcessItem(IFileSystemItem item, int level)
			{
				Files.Add(new FileNode(item, _rootFolder));
			}
		}
	}
}