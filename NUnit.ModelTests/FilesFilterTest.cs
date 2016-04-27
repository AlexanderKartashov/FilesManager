using Model.Core;
using Model.Processing;
using NUnit.Framework;

namespace NUnit.ModelTests
{
	[TestFixture]
	public class FilesFilterTest
	{
		private FilesFilter _filter;
		private IFileSystemItem _object;

		[SetUp]
		public void SetUp()
		{
			_filter = new FilesFilter();
		}

		[Test]
		public void TestFilterOnFolder()
		{
			_object = new Folder("folder");
			Assert.That(_filter.FilterItem(_object, 0), Is.False);
		}

		[Test]
		public void TestFilterOnFile()
		{
			_object = new File("file");
			Assert.That(_filter.FilterItem(_object, 0), Is.True);
		}
	}
}
