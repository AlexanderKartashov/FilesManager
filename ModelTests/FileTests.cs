using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTests
{
    [TestClass]
    public class FileTests
    {
        [TestMethod]
        public void TestGetters()
        {
            const String expectedName = "expected";
            const int expectedSize = 42;

            File file = new File(expectedName, expectedSize);

            Assert.AreEqual(expectedName, file.Name);
            Assert.AreEqual(expectedSize, file.Size);
            Assert.IsNull(file.Objects);
        }
    }
}
