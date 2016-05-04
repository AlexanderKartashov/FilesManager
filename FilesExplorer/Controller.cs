using Model.Core;
using Model.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace FilesExplorer
{
	class Controller
	{
#region events
		public event Action ModelCreationCompleteEvent;
		public event Action<String> ModelCreationProgressEvent;
		#endregion

#region classes
		public class FilteredProcessor
		{
			public IList<IItemFilter> Filters { get; set; }
			public IItemProcessor Processor { get; set; }
		}

		private class Result
		{
			public IFileSystemItem Model { get; set; }
			public String ErrorMessage { get; set; }
		}
#endregion

		public String RootPath { get; set; }

		private BackgroundWorker _bw = new BackgroundWorker();
		private IList<FilteredProcessor> _filteredProcessors;

		public Controller()
		{
			_bw.DoWork += _bw_DoWork;
			_bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
			_bw.ProgressChanged += _bw_ProgressChanged;
			_bw.WorkerReportsProgress = true;
		}

		public void CreateModel(IList<FilteredProcessor> filteredProcessors)
		{
			_filteredProcessors = filteredProcessors;
			_bw.RunWorkerAsync(RootPath);
		}

		private void _bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ModelCreationProgressEvent?.Invoke(e.UserState as String);
		}

		private void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ModelCreationCompleteEvent?.Invoke();

			var result = e.Result as Result;
			if (result.Model == null)
			{
				MessageBox.Show(result.ErrorMessage);
			}
			else
			{
				var itemsProcessor = new ItemsProcessor();
				_filteredProcessors?.ToList().ForEach((item) => itemsProcessor.AddProcessorStrategy(item.Processor, item.Filters));
				itemsProcessor.Process(result.Model, new EnumerateInDepth());
			}
		}

		private void _bw_DoWork(object sender, DoWorkEventArgs e)
		{
			String path = (String)e.Argument;
			var bw = sender as BackgroundWorker;
			IFileSystemItem model = null;

			try
			{
				var _modelCreator = new ModelCreator();
				_modelCreator.ItemAddedInModelEvent += (String itemPath, bool isFolder) => {
					if (isFolder)
					{
						bw.ReportProgress(0, itemPath);
					}
				};
				model = _modelCreator.GetFileSystemIerarchy(path);
			}
			catch (Exception ex)
			{
				e.Result = new Result { Model = null, ErrorMessage = ex.Message };
				return;
			}

			e.Result = new Result { Model = model, ErrorMessage = null };
		}
	}
}
