using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTests
{
    [TestClass]
    public class FileSystemItemBaseTests
    {
        private class FileSystemItemBaseDerived : FileSystemItemBase
        {
            public FileSystemItemBaseDerived(String path, int size, DateTime creationDate, DateTime modificationDate)
                : base(path, size, creationDate, modificationDate)
            {
            }

            public override IEnumerable<IFileSystemItem> Objects
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }


        [TestMethod]
        public void TestGetters()
        {
            var expectedName = "expected";
            var expectedSize = 42;
            var expectedCreationDate = DateTime.Today;
            var expectedModificationDate = DateTime.Now;

            var file = new FileSystemItemBaseDerived(expectedName, expectedSize, expectedCreationDate, expectedModificationDate);

            Assert.AreEqual(expectedName, file.Path);
            Assert.AreEqual(expectedSize, file.Size);
            Assert.AreEqual(expectedCreationDate, file.CreationDate);
            Assert.AreEqual(expectedModificationDate, file.ModificationDate);
        }
    }
}
