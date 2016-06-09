using AgoApp.Model;
using AgoApp.ViewModel;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Security;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows.Input;

namespace AgoApp.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        public LogoutCommand()
        {
            _viewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            _viewModel.PropertyChanged += (sender, e) =>
            {
                CanExecuteChanged?.Invoke(sender, e);
            };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return null != _viewModel.PortalConnection && null != _viewModel.PortalConnection.TokenCredential;
        }

        public async void Execute(object parameter)
        {
            // Remove the credential
            foreach (var credential in IdentityManager.Current.Credentials)
            {
                if (credential.ServiceUri.StartsWith(_viewModel.PortalConnection.Portal.Uri.AbsoluteUri))
                {
                    IdentityManager.Current.RemoveCredential(credential);
                }
            }

            // Access the portal anonymous
            var portal = await ArcGISPortal.CreateAsync(_viewModel.PortalConnection.Portal.Uri);
            _viewModel.PortalConnection = new PortalConnection(portal);
        }
    }
}
