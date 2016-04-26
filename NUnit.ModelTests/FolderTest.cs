using NUnit.Framework;
using Model.Core;
using System.IO;

namespace NUnit.ModelTests
{
    [TestFixture]
    public class FolderTest
    {
        [Test]
        public void TestFolder()
        {
            var folder = new Folder("folder");

            Assert.That(folder.Objects, Is.Not.Null);
            Assert.That(folder.Info, Is.Not.Null);
            Assert.That(folder.Info, Is.InstanceOf<DirectoryInfo>());

            var enumerator = folder.Objects.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.False);

            Assert.That(() => folder.Add(new Model.Core.File("file")), Throws.Nothing);

			Assert.That(folder.Objects, Is.Not.Null);
            enumerator = folder.Objects.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.True);
            var curr = enumerator.Current;
            Assert.That(curr, Is.Not.Null);
            Assert.That(curr.Objects, Is.Null);
        }
    }
}
