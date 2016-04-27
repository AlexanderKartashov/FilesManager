using Alphaleonis.Win32.Filesystem;
using System;

namespace NUnit.ModelTests
{
	internal class FileSystemTestsInitializer
	{
		private const String _dir = "dir";
		private const String _subDir1 = "subdir1";
		private const String _subDir2 = "subdir2";
		private const String _subDir3 = "subdir3";
		private const String _subSubDir = "subsubdir";
		private const String _file1 = "file1";
		private const String _file2 = "file2";
		private const String _subDir1File = "subdir1file";
		private const String _subSubDirFile = "subsubdirfile";

		public String initialPath;
		public String subDir1Path;
		public String subDir2Path;
		public String subDir3Path;
		public String subSubDirPath;
		public String file1Path;
		public String file2Path;
		public String subDir1FilePath;
		public String subSubDirFilePath;

		/*
		 initialPath
			subDir1Path
				subDir1FilePath
			subDir2Path
				subSubDirPath
					subSubDirFilePath
			subDir3Path
			file1Path
			file2Path
		*/
		public void Init()
		{
			var currentDir = Directory.GetCurrentDirectory();

			initialPath = Path.Combine(currentDir, _dir);
			Directory.CreateDirectory(initialPath);

			subDir1Path = Path.Combine(initialPath, _subDir1);
			Directory.CreateDirectory(subDir1Path);

			subDir2Path = Path.Combine(initialPath, _subDir2);
			Directory.CreateDirectory(subDir2Path);

			subDir3Path = Path.Combine(initialPath, _subDir3);
			Directory.CreateDirectory(subDir3Path);

			subSubDirPath = Path.Combine(subDir2Path, _subSubDir);
			Directory.CreateDirectory(subSubDirPath);

			file1Path = Path.Combine(initialPath, _file1);
			using (System.IO.File.Create(file1Path))
			{ }

			file2Path = Path.Combine(initialPath, _file2);
			using (System.IO.File.Create(file2Path))
			{ }

			subDir1FilePath = Path.Combine(subDir1Path, _subDir1File);
			using (System.IO.File.Create(subDir1FilePath))
			{ }

			subSubDirFilePath = Path.Combine(subSubDirPath, _subSubDirFile);
			using (System.IO.File.Create(subSubDirFilePath))
			{ }
		}

		public void Delete()
		{
			Directory.Delete(initialPath, true);
		}
	}
}
