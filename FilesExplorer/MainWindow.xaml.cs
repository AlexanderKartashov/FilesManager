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

namespace FilesExplorer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
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
			progressLayer.Visibility = Visibility.Visible;
			_controller.CreateModel();
		}
	}
}
