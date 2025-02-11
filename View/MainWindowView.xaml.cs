using System.Windows;
using Reserve_iT.ViewModel;

namespace Reserve_iT.View
{
	/// <summary>
	/// Interaktionslogik für MainWindowView.xaml
	/// </summary>
	public partial class MainWindowView : Window
	{
		public MainWindowView()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var view = new DashboardView();
			view.DataContext = DataContext;
			((MainWindowViewModel)DataContext).MainFrame = MainFrame; // Mainframe aus XAML in ViewModel durchreichen
			MainFrame.Navigate(view);
		}

    private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {

        }
    }
}
