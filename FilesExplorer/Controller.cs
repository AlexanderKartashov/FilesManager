using Model.Core;
using Model.Processing;
using System;
using System.ComponentModel;
using System.Windows;

namespace FilesExplorer
{
	public class Controller
	{
		public event Action ModelCreationCompleteEvent;
		public event Action<String> ModelCreationProgressEvent;

		public String RootPath { get; set; }

		class Result
		{
			public IFileSystemItem Model { get; set; }
			public String ErrorMessage { get; set; }
		}

		private BackgroundWorker _bw = new BackgroundWorker();
		private ItemsProcessor _itemsProcessor = new ItemsProcessor();

		public Controller()
		{
			_bw.DoWork += _bw_DoWork;
			_bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
			_bw.ProgressChanged += _bw_ProgressChanged;
			_bw.WorkerReportsProgress = true;
		}

		public void CreateModel()
		{
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
				// todo
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
