using Model;
using NUnit.Framework;
using System.IO;

namespace NUnit.ModelTests
{
    [TestFixture]
    public class FileTest
    {
        [Test]
        public void TestMethod()
        {
            var file = new Model.File("file");

            Assert.IsNull(file.Objects);
            Assert.NotNull(file.Info);
            Assert.IsInstanceOf<FileInfo>(file.Info);
        }
    }
}
