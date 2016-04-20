using System;
using System.IO;
using System.Linq;

namespace Model
{
    public class FileSystemEnumerator
    {
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
            var folder = new Folder(path);
            var folderInfo = folder.Info as DirectoryInfo;

            folderInfo.GetDirectories().ToList().ForEach((DirectoryInfo dir) => folder.Add(GetFolderIerarchy(dir.FullName)));
            folderInfo.GetFiles().ToList().ForEach((FileInfo file) => folder.Add(new File(file.FullName)));

            return folder;
        }
    }
}
