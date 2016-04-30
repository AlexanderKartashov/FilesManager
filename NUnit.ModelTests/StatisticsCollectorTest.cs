using Model.Core;
using Model.Processing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.ModelTests
{
	[TestFixture]
	public class StatisticsCollectorTest
	{
		private FileSystemTestsInitializer _initializer;
		private ModelCreator _creator;
		private ItemsProcessor _processor;
		private StatisticsCollector _collector;
		private IFileSystemItem _model;

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
			_collector = new StatisticsCollector();

			_processor = new ItemsProcessor();
			_processor.AddProcessorStrategy(_collector);

			_creator = new ModelCreator();
			_model = _creator.GetFileSystemIerarchy(_initializer.initialPath);
		}

		[Test]
		public void TestCollector()
		{
			Assert.That(() => _processor.Process(_model, new EnumerateInDepth()), Throws.Nothing);

			Assert.That(_collector.FilesCount, Is.EqualTo(4));
			Assert.That(_collector.Extensions, Is.EquivalentTo(new SortedSet<string>() { ".bat", ".exe", ".txt" }));
			Assert.That(_collector.TotalSize, Is.EqualTo(0));
		}
	}
}
