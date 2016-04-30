using Alphaleonis.Win32.Filesystem;
using Model.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnit.ModelTests
{
	[TestFixture]
    public class ModelCreatorTest
    {
		private FileSystemTestsInitializer _initializer;
		private ModelCreator _creator;

		class TestData
		{
			public String Path { get; set; }
			public bool IsFolder { get; set; }
		}

		private List<TestData> _paths;
		private IEnumerator<TestData> _pathEnumerator;

		[OneTimeSetUp]
        public void OneTimeSetUp()
        {
			_initializer = new FileSystemTestsInitializer();
			_initializer.Init();
		}

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
			_initializer.Delete();
        }

        [SetUp]
        public void SetUp()
        {
            _creator = new ModelCreator();
			_creator.ItemAddedInModelEvent += _creator_ItemAddedInModelEvent;

			_paths = new List<TestData>
			{
				new TestData() { Path = _initializer.initialPath, IsFolder = true },
				new TestData() { Path = _initializer.subDir1Path, IsFolder = true },
				new TestData() { Path = _initializer.subDir1FilePath, IsFolder = false },
				new TestData() { Path = _initializer.subDir2Path, IsFolder = true },
				new TestData() { Path = _initializer.subSubDirPath, IsFolder = true },
				new TestData() { Path = _initializer.subSubDirFilePath, IsFolder = false },
				new TestData() { Path = _initializer.subDir3Path, IsFolder = true },
				new TestData() { Path = _initializer.file1Path, IsFolder = false },
				new TestData() { Path = _initializer.file2Path, IsFolder = false }
			};
			_pathEnumerator = _paths.GetEnumerator();
			_pathEnumerator.Reset();
			_pathEnumerator.MoveNext();
		}

		[TearDown]
		public void TearDown()
		{
			_creator.ItemAddedInModelEvent -= _creator_ItemAddedInModelEvent;
		}

		[Test]
        public void ThrowsExceptionIfDirectoryNotExists()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var notExistingDir = Path.Combine(currentDir, "not existing directory");

            Assert.That(() => _creator.GetFileSystemIerarchy(notExistingDir), Throws.ArgumentException);
        }

        [Test]
        public void ThrowsExceptionIfItemNotDirectory()
        {
			Assert.That(() => _creator.GetFileSystemIerarchy(_initializer.subDir1FilePath), Throws.ArgumentException);
        }

		private void _creator_ItemAddedInModelEvent(string itemPath, bool isFolder)
		{
			var testItem = _pathEnumerator.Current;
			Assert.That(itemPath, Is.EqualTo(testItem.Path));
			Assert.That(isFolder, Is.EqualTo(testItem.IsFolder));
			_pathEnumerator.MoveNext();
		}

		private void TestFolder(IFileSystemItem item, String expectedPath, int expectedItemsCount)
		{
			Assert.That(item, Is.InstanceOf<Folder>());
			Assert.That(item.Info, Is.Not.Null);
			Assert.That(item.Info.FullName, Is.EqualTo(expectedPath));
			IEnumerable<IFileSystemItem> objects = null;
			Assert.That(() => objects = item.Objects, Throws.Nothing);
			Assert.That(objects, Is.Not.Null);
			Assert.That(objects.Count(), Is.EqualTo(expectedItemsCount));
		}

		private void TestFile(IFileSystemItem item, String expectedPath)
		{
			Assert.That(item, Is.InstanceOf<Model.Core.File>());
			Assert.That(item.Info, Is.Not.Null);
			Assert.That(item.Info.FullName, Is.EqualTo(expectedPath));
			IEnumerable<IFileSystemItem> objects = null;
			Assert.That(() => objects = item.Objects, Throws.Nothing);
			Assert.That(objects, Is.Null);
		}

		[Test]
		public void TestModelCreator()
		{
			IFileSystemItem model = null;
			Assert.That(() => model = _creator.GetFileSystemIerarchy(_initializer.initialPath), Throws.Nothing);
			Assert.That(model, Is.Not.Null);

			Assert.That(model, Is.InstanceOf<Folder>());
			TestFolder(model, _initializer.initialPath, 5);

			var list = model.Objects.ToList();
			var subDir1 = list[0];
			TestFolder(subDir1, _initializer.subDir1Path, 1);

			var subDir1File = subDir1.Objects.ToList()[0];
			TestFile(subDir1File, _initializer.subDir1FilePath);

			var subDir2 = list[1];
			TestFolder(subDir2, _initializer.subDir2Path, 1);

			var subSubDir = subDir2.Objects.ToList()[0];
			TestFolder(subSubDir, _initializer.subSubDirPath, 1);

			var subSubDirFile = subSubDir.Objects.ToList()[0];
			TestFile(subSubDirFile, _initializer.subSubDirFilePath);

			var subDir3 = list[2];
			TestFolder(subDir3, _initializer.subDir3Path, 0);

			var file1 = list[3];
			TestFile(file1, _initializer.file1Path);

			var file2 = list[4];
			TestFile(file2, _initializer.file2Path);
		}
    }
}
