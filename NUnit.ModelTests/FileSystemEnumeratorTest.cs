using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.ModelTests
{
	[TestFixture]
	public class FileSystemEnumeratorTest
	{
		private FileSystemTestsInitializer _initializer;
		private ModelCreator _creator;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_initializer = new FileSystemTestsInitializer();
			_initializer.Init();
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_initializer.Delete();
		}

		[SetUp]
		public void SetUp()
		{
			_creator = new ModelCreator();
		}

		[Test]
		public void TestMethod()
		{
			// TODO: Add your test code here
			Assert.Pass("Your first passing test");
		}
	}
}
