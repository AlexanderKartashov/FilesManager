using Alphaleonis.Win32.Filesystem;
using System;
using System.Linq;

namespace Model
{
	namespace Core
	{
		public class ModelCreator
		{
			public delegate void ItemAddedInModelHandler(String itemPath, bool isFolder);
			public event ItemAddedInModelHandler ItemAddedInModelEvent;

			public IFileSystemItem GetFileSystemIerarchy(String initialPath)
			{
				if (!Directory.Exists(initialPath))
				{
					throw new ArgumentException("directory not exists, or path not a directory");
				}

				return GetFolderIerarchy(initialPath);
			}

			private IFileSystemItem GetFolderIerarchy(String path)
			{
				DispatchEvent(path, true);

				var folder = new Folder(path);
				var folderInfo = folder.Info as DirectoryInfo;

				folderInfo.GetDirectories().ToList().ForEach((DirectoryInfo dir) =>
				{
					folder.Add(GetFolderIerarchy(dir.FullName));
				});

				folderInfo.GetFiles().ToList().ForEach((FileInfo file) =>
				{
					DispatchEvent(file.FullName, false);
					folder.Add(new File(file.FullName));
				});

				return folder;
			}

			private void DispatchEvent(String path, bool isFolder)
			{
				ItemAddedInModelEvent?.Invoke(path, isFolder);
			}
		} 
	}
}
