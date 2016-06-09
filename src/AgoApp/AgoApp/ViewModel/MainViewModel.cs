using AgoApp.Commands;
using AgoApp.Model;
using Esri.ArcGISRuntime.Portal;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using System.Windows.Input;

namespace AgoApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private MetroWindow _mainWindow;
        private ICommand _showLoginCommand;
        private ICommand _logoutCommand;
        private PortalConnection _portalConnection;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                ConnectAsync();
            }
        }

        private async void ConnectAsync()
        {
            // Access portal anonymously
            var portal = await ArcGISPortal.CreateAsync();
            PortalConnection = new PortalConnection(portal);
        }

        public MetroWindow MainWindow
        {
            get { return _mainWindow; }
            set { Set(ref _mainWindow, value); }
        }

        public ICommand ShowLoginCommand
        {
            get
            {
                return _showLoginCommand ?? (_showLoginCommand = new ShowLoginCommand());
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new LogoutCommand());
            }
        }

        public PortalConnection PortalConnection
        {
            get { return _portalConnection; }
            set { Set(ref _portalConnection, value); }
        }
    }
}