using Model.Core;
using Model.Processing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.ModelTests
{
	[TestFixture]
	public class ItemsProcessorTest
	{
		private FileSystemTestsInitializer _initializer;
		private ModelCreator _creator;
		private ItemsProcessor _processor;

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
			_processor = new ItemsProcessor();
		}

		[Test]
		public void ThrowsExceptionIfNotProcessingStrategy()
		{
			var model = _creator.GetFileSystemIerarchy(_initializer.initialPath);
			Assert.That(() => _processor.Process(model, new Mock<IEnumerationStrategy>().Object), Throws.InvalidOperationException);
		}

		[Test]
		public void ThrowsExceptionIfModelIsNull()
		{
			_processor.AddProcessorStrategy(new Mock<IItemProcessor>().Object);
			Assert.That(() => _processor.Process(null, new Mock<IEnumerationStrategy>().Object), Throws.ArgumentNullException);
		}

		[Test]
		public void ThrowsExceptionIfEnumerationStrategyNotSet()
		{
			var model = _creator.GetFileSystemIerarchy(_initializer.initialPath);
			_processor.AddProcessorStrategy(new Mock<IItemProcessor>().Object);
			Assert.That(() => _processor.Process(model, null), Throws.ArgumentNullException);
		}
	}
}
