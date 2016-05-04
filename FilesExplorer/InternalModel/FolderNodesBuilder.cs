using System;
using System.Collections.Generic;
using System.Linq;
using Model.Core;
using Model.Processing;

namespace FilesExplorer
{
	namespace InternalModel
	{
		public class FolderNodesBuilder : IItemProcessor
		{
			public List<FolderNode> Root { get; set; } = new List<FolderNode>();

			private List<FolderNode> _hierarchy = new List<FolderNode>();
			private int _level = 0;
			private String _rootFolder;

			public FolderNodesBuilder(String rootFolder)
			{
				_rootFolder = rootFolder;
			}

			public void ProcessItem(IFileSystemItem item, int level)
			{
				if (item is Folder)
				{
					if (Root.Count == 0)
					{
						var root = new FolderNode(item, _rootFolder);
						Root.Add(root);
						_hierarchy.Add(root);
						_level = level;
					}
					else
					{
						var newFolder = new FolderNode(item, _rootFolder);

						if (level < _level)
						{
							int diff = _level - level;
							for (var i = 0; i < diff; ++i)
							{
								_hierarchy.RemoveAt(_hierarchy.Count - 1);
							}

						}

						_hierarchy.Last().Items.Add(newFolder);
						_hierarchy.Add(newFolder);
						_level = level;
					}
				}
				else if (item is File)
				{
					_hierarchy.Last().Items.Add(new FileNode(item, _rootFolder));
				}
				else
				{
					throw new ArgumentException("Invalid item type");
				}
			}
		}
	}
}