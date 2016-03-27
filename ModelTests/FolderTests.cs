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
            const String expectedName = "expected";
            const int expectedSize = 42;

            Folder folder = new Folder(expectedName, expectedSize);
            Assert.AreEqual(expectedName, folder.Name);
            Assert.AreEqual(expectedSize, folder.Size);
            Assert.IsNotNull(folder.Objects);

            var enumerator = folder.Objects.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());

            const String expectedFileName = "fileName";
            const int expectedFileSize = 43;
            folder.Add(new File(expectedFileName, expectedFileSize));

            Assert.IsNotNull(folder.Objects);
            enumerator = folder.Objects.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            var curr = enumerator.Current;
            Assert.IsNotNull(curr);
            Assert.AreEqual(expectedFileName, curr.Name);
            Assert.AreEqual(expectedFileSize, curr.Size);
            Assert.IsNull(curr.Objects);
        }
    }
}
