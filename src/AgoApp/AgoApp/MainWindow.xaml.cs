using AgoApp.ViewModel;
using MahApps.Metro.Controls;

namespace AgoApp
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = (MainViewModel)DataContext;
            viewModel.MainWindow = this;
        }
    }
}
