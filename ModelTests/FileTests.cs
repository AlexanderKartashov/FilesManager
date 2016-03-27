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
            var file = new File("name", 0, DateTime.Today, DateTime.Now);

            Assert.IsNull(file.Objects);
        }
    }
}
