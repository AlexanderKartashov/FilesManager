using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	namespace Processing
	{
		public class ItemsProcessor
		{

#region nested types
			private struct FilteredProcessor
			{
				public FilteredProcessor(IItemProcessor processor, IList<IItemFilter> filters)
				{
					Processor = processor;
					Filters = filters;
				}

				public IItemProcessor Processor { get; }
				public IList<IItemFilter> Filters { get; }
			}

			private class EnumeratorContainer : IDisposable
			{
				FileSystemEnumerator _enumerator = new FileSystemEnumerator();
				FileSystemEnumerator.ProcessItemEventHandler _eventHandler;

				public EnumeratorContainer(FileSystemEnumerator.ProcessItemEventHandler handler)
				{
					_eventHandler = handler;
					_enumerator.ProcessItemEvent += _eventHandler;
				}

				public void Enumerate(IFileSystemItem root)
				{
					_enumerator.Enumerate(root);
				}

				public void Dispose()
				{
					_enumerator.ProcessItemEvent -= _eventHandler;
				}
			}
#endregion

			private List<FilteredProcessor> _filteredProcessors = new List<FilteredProcessor>();

			public void AddProcessorStrategy(IItemProcessor processor, IList<IItemFilter> filters = null)
			{
				if (processor == null)
				{
					throw new ArgumentNullException("processor msut be not null");
				}

				_filteredProcessors.Add(new FilteredProcessor(processor, filters));
			}

			public void Process(IFileSystemItem root)
			{
				if (_filteredProcessors.Count == 0)
				{
					throw new InvalidOperationException("add at least one processing strategy");
				}

				using (var enumerator = new EnumeratorContainer(Enumerator_ProcessItemEvent))
				{
					enumerator.Enumerate(root);
				}
			}

			private void Enumerator_ProcessItemEvent(IFileSystemItem item, int level)
			{
				_filteredProcessors.ForEach((FilteredProcessor processorWithFilter) => {
					bool filtered = processorWithFilter.Filters?.All((IItemFilter filter) => filter.FilterItem(item, level)) ?? true;
					if (filtered)
					{
						processorWithFilter.Processor.ProcessItem(item, level);
					}
				});
			}
		}
	}
}
