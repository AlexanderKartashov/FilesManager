using System;
using System.IO;
using System.Linq;

namespace Model
{
    public class FileSystemEnumerator
    {
        public IFileSystemItem GetFileSystemIerarchy(String initialPath)
        {
            var attr = System.IO.File.GetAttributes(initialPath);
            if (!attr.HasFlag(FileAttributes.Directory))
            {
                throw new Exception();
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
