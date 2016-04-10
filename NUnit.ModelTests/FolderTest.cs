using NUnit.Framework;
using Model;
using System.IO;

namespace NUnit.ModelTests
{
    [TestFixture]
    public class FolderTest
    {
        [Test]
        public void TestMethod()
        {
            var folder = new Folder("folder");

            Assert.IsNotNull(folder.Objects);
            Assert.NotNull(folder.Info);
            Assert.IsInstanceOf<DirectoryInfo>(folder.Info);

            var enumerator = folder.Objects.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());

            folder.Add(new Model.File("file"));

            Assert.IsNotNull(folder.Objects);
            enumerator = folder.Objects.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            var curr = enumerator.Current;
            Assert.IsNotNull(curr);
            Assert.IsNull(curr.Objects);
        }
    }
}
