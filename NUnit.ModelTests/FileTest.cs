using Alphaleonis.Win32.Filesystem;
using NUnit.Framework;

namespace NUnit.ModelTests
{
    [TestFixture]
    public class FileTest
    {
        [Test]
        public void TestFile()
        {
			var file = new Model.Core.File("file");

            Assert.That(file.Objects, Is.Null);
            Assert.That(file.Info, Is.Not.Null);
            Assert.That(file.Info, Is.InstanceOf<FileInfo>());
        }
    }
}
