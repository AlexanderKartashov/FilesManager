using Model.Core;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace FilesExplorer
{
	namespace InternalModel
	{
		public abstract class BaseNode
		{
			protected IFileSystemItem _item;
			protected Uri _rootPath;

			public ObservableCollection<BaseNode> Items { get; } = new ObservableCollection<BaseNode>();

			public BaseNode(IFileSystemItem item, String rootPath)
			{
				_rootPath = new Uri(rootPath);
				_item = item;
			}

			public String AbsolutePath { get { return _item.Info.FullName; } }
			public String RelativePath { get { return Uri.UnescapeDataString(_rootPath.MakeRelativeUri(new Uri(_item.Info.FullName)).ToString()); } }
			public DateTime CreationTime { get { return _item.Info.CreationTime; } }
			public DateTime LastWriteTime { get { return _item.Info.LastWriteTime; } }
			public String Extension { get { return _item.Info.Extension; } }
			public String Name { get { return _item.Info.Name; } }
		}
	}
}