using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTests
{
    [TestClass]
    public class FolderTests
    {
        [TestMethod]
        public void TestGetters()
        {
            var folder = new Folder("name", 0, DateTime.Today, DateTime.Now);

            Assert.IsNotNull(folder.Objects);

            var enumerator = folder.Objects.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());

            var expectedFileName = "fileName";
            var expectedFileSize = 43;
            var expectedCreationDate = DateTime.Today;
            var expectedModificationdDate = DateTime.Now;

            folder.Add(new File(expectedFileName, expectedFileSize, expectedCreationDate, expectedModificationdDate));

            Assert.IsNotNull(folder.Objects);
            enumerator = folder.Objects.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            var curr = enumerator.Current;
            Assert.IsNotNull(curr);
            Assert.AreEqual(expectedFileName, curr.Path);
            Assert.AreEqual(expectedFileSize, curr.Size);
            Assert.AreEqual(expectedCreationDate, curr.CreationDate);
            Assert.AreEqual(expectedModificationdDate, curr.ModificationDate);
            Assert.IsNull(curr.Objects);
        }
    }
}
