using System.Windows;

namespace ZE.UI.WPF.Spy
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      ViewModel = new MainViewModel();
    }

    public MainViewModel ViewModel
    {
      get
      {
        return DataContext as MainViewModel;
      }

      set
      {
        DataContext = value;
      }
    }
  }
}
