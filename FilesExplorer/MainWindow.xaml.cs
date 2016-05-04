using FilesExplorer.InternalModel;
using Model.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model.Core;
using System.Collections.ObjectModel;

namespace FilesExplorer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public FolderNodesBuilder FoldersHierarchy { get; set; }
		public FilesListBuilder FilesList { get; set; }

		private Controller _controller = new Controller();

		public MainWindow()
		{
			InitializeComponent();
			InitBindings();
		}

		private void InitBindings()
		{
			rootFolderText.DataContext = _controller;

			_controller.ModelCreationCompleteEvent += _controller_ModelCreationCompleteEvent;
			_controller.ModelCreationProgressEvent += _controller_ModelCreationProgressEvent;
		}

		private void _controller_ModelCreationProgressEvent(string obj)
		{
			progressDescription.Text = String.Format("Processing folder {0}", obj);
		}

		private void _controller_ModelCreationCompleteEvent()
		{
			progressLayer.Visibility = Visibility.Hidden;
		}

		private void viewButton_onClick(object sender, RoutedEventArgs e)
		{
			FilesList = new FilesListBuilder(_controller.RootPath);
			FoldersHierarchy = new FolderNodesBuilder(_controller.RootPath);

			progressLayer.Visibility = Visibility.Visible;
			_controller.CreateModel(new List<Controller.FilteredProcessor>()
			{
				new Controller.FilteredProcessor() { Filters = new List<IItemFilter>() { new FilesFilter() }, Processor = FilesList },
				new Controller.FilteredProcessor() { Processor = FoldersHierarchy }
			});

			treeView.ItemsSource = FoldersHierarchy.Root;
			filesList.ItemsSource = FilesList.Files;
		}
	}
}
