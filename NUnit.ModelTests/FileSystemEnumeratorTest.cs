using Model.Core;
using Model.Processing;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NUnit.ModelTests
{
	[TestFixture]
	public class FileSystemEnumeratorTest
	{
		private FileSystemTestsInitializer _initializer;
		private ModelCreator _creator;

		class ExpectedTestData
		{
			public int ExpectedLevel { get; set; }
			public String ExpectedPath { get; set; }
			public Type ExpectedType { get; set; }
		}
		private List<ExpectedTestData> _testItems;
		private IEnumerator<ExpectedTestData> _testItemEnumerator;

		private FileSystemEnumerator _enumerator;

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
			var mockStrat = new Moq.Mock<IEnumerationStrategy>();
			_enumerator = new FileSystemEnumerator(mockStrat.Object);
		}

		[TearDown]
		public void TearDown()
		{
			_enumerator.ProcessItemEvent -= _enumerator_ProcessItemEvent;
		}

		[Test]
		public void ThrowsExceptionIfItemIsNull()
		{
			Assert.That(() => _enumerator.Enumerate(null), Throws.ArgumentNullException);
		}

		[Test]
		public void ThrowsExceptionIfEnumerationStrategyIsNull()
		{
			Assert.That(() => new FileSystemEnumerator(null), Throws.ArgumentNullException);
		}

		[Test]
		public void TestFilesFirstEnumeration()
		{
			FileSystemEnumerator enumerator = new FileSystemEnumerator(new EnumerateFilesFirst());
			enumerator.ProcessItemEvent += _enumerator_ProcessItemEvent;

			_testItems = new List<ExpectedTestData>
			{
				new ExpectedTestData() { ExpectedLevel = 0, ExpectedPath = _initializer.initialPath, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.file1Path, ExpectedType = typeof(File) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.file2Path, ExpectedType = typeof(File) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.subDir1Path, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 2, ExpectedPath = _initializer.subDir1FilePath, ExpectedType = typeof(File) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.subDir2Path, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 2, ExpectedPath = _initializer.subSubDirPath, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 3, ExpectedPath = _initializer.subSubDirFilePath, ExpectedType = typeof(File) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.subDir3Path, ExpectedType = typeof(Folder) }
			};
			_testItemEnumerator = _testItems.GetEnumerator();
			_testItemEnumerator.Reset();
			_testItemEnumerator.MoveNext();

			var model = _creator.GetFileSystemIerarchy(_initializer.initialPath);
			Assert.That(() => enumerator.Enumerate(model), Throws.Nothing);
		}

		[Test]
		public void TestInDepthEnumeration()
		{
			FileSystemEnumerator enumerator = new FileSystemEnumerator(new EnumerateInDepth());
			enumerator.ProcessItemEvent += _enumerator_ProcessItemEvent;

			_testItems = new List<ExpectedTestData>
			{
				new ExpectedTestData() { ExpectedLevel = 0, ExpectedPath = _initializer.initialPath, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.subDir1Path, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 2, ExpectedPath = _initializer.subDir1FilePath, ExpectedType = typeof(File) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.subDir2Path, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 2, ExpectedPath = _initializer.subSubDirPath, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 3, ExpectedPath = _initializer.subSubDirFilePath, ExpectedType = typeof(File) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.subDir3Path, ExpectedType = typeof(Folder) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.file1Path, ExpectedType = typeof(File) },
				new ExpectedTestData() { ExpectedLevel = 1, ExpectedPath = _initializer.file2Path, ExpectedType = typeof(File) }
			};
			_testItemEnumerator = _testItems.GetEnumerator();
			_testItemEnumerator.Reset();
			_testItemEnumerator.MoveNext();

			var model = _creator.GetFileSystemIerarchy(_initializer.initialPath);
			Assert.That(() => enumerator.Enumerate(model), Throws.Nothing);
		}

		private void _enumerator_ProcessItemEvent(IFileSystemItem item, int level)
		{
			var testItem = _testItemEnumerator.Current;
			Assert.That(level, Is.EqualTo(testItem.ExpectedLevel));
			Assert.That(item.Info.FullName, Is.EqualTo(testItem.ExpectedPath));
			Assert.That(item, Is.InstanceOf(testItem.ExpectedType));
			_testItemEnumerator.MoveNext();
		}
	}
}
