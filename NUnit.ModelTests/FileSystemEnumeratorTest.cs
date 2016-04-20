using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.ModelTests
{
    [TestFixture]
    public class FileSystemEnumeratorTest
    {
        private const String _dir = "dir";
        private const String _subDir1 = "subdir1";
        private const String _subDir2 = "subdir2";
        private const String _subSubDir = "subsubdir";
        private const String _file1 = "file1";
        private const String _file2 = "file2";
        private const String _subDir1File = "subdir1file";
        private const String _subSubDirFile = "subsubdirfile";

        private String _initialPath;
        private String _subDir1Path;
        private String _subDir2Path;
        private String _subSubDirPath;
        private String _file1Path;
        private String _file2Path;
        private String _subDir1FilePath;
        private String _subSubDirFilePath;

        private FileSystemEnumerator _enumerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var currentDir = Directory.GetCurrentDirectory();

            _initialPath = Path.Combine(currentDir, _dir);
            Directory.CreateDirectory(_initialPath);

            _subDir1Path = Path.Combine(_initialPath, _subDir1);
            Directory.CreateDirectory(_subDir1Path);

            _subDir2Path = Path.Combine(_initialPath, _subDir2);
            Directory.CreateDirectory(_subDir2Path);

            _subSubDirPath = Path.Combine(_subDir1Path, _subSubDir);
            Directory.CreateDirectory(_subSubDirPath);

			_file1Path = Path.Combine(_initialPath, _file1);
            using (System.IO.File.Create(_file1Path))
            {}

            _file2Path = Path.Combine(_initialPath, _file2);
            using (System.IO.File.Create(_file2Path))
            {}

            _subDir1FilePath = Path.Combine(_subDir1Path, _subDir1File);
            using (System.IO.File.Create(_subDir1FilePath))
            {}

			_subSubDirFilePath = Path.Combine(_subSubDirPath, _subSubDirFile);
            using (System.IO.File.Create(_subSubDirFilePath))
            {}
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Directory.Delete(_initialPath, true);
        }

        [SetUp]
        public void SetUp()
        {
            _enumerator = new FileSystemEnumerator();
        }

        [Test]
        public void ThrowsExceptionIfDirectoryNotExists()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var notExistingDir = Path.Combine(currentDir, "not existing directory");

            Assert.That(() => _enumerator.GetFileSystemIerarchy(notExistingDir), Throws.ArgumentException);
        }

        [Test]
        public void ThrowsExceptionIfItemNotDirectory()
        {
            var enumerator = new FileSystemEnumerator();

            Assert.That(() => _enumerator.GetFileSystemIerarchy(_subDir1FilePath), Throws.ArgumentException);
        }
    }
}
