using Model.Core;
using Alphaleonis.Win32.Filesystem;
using System;

namespace FilesExplorer
{
	namespace InternalModel
	{
		public class FileNode : BaseNode
		{
			public FileNode(IFileSystemItem file, String rootPath)
				: base(file, rootPath)
			{
			}

			public long Size
			{
				get
				{
					return (File.Info as FileInfo).Length;
				}
			}

			private Model.Core.File File { get { return _item as Model.Core.File; } }
		}
	}
}