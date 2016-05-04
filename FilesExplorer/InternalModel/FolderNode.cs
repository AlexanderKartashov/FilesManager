using Model.Core;
using System;
using System.Linq;

namespace FilesExplorer
{
	namespace InternalModel
	{
		public class FolderNode : BaseNode
		{
			public FolderNode(IFileSystemItem folder, String rootPath)
				: base(folder, rootPath)
			{
				InitItems();
			}

			private void InitItems()
			{
				var objects = Folder.Objects;

				objects?.OfType<Folder>().ToList().ForEach((folder) => Items.Add(new FolderNode(folder, _rootPath.ToString())));
				objects?.OfType<File>().ToList().ForEach((file) => Items.Add(new FileNode(file, _rootPath.ToString())));
			}

			private Folder Folder { get { return _item as Folder; } }

			public new String RelativePath
			{
				get
				{
					var relativePath = Uri.UnescapeDataString(_rootPath.MakeRelativeUri(new Uri(_item.Info.FullName)).ToString());
					return relativePath.Length == 0 ? "..." : relativePath;
				}
			}
		}
	}
}