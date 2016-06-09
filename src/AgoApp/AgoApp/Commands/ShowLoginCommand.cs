using AgoApp.Model;
using AgoApp.ViewModel;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Security;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows.Input;

namespace AgoApp.Commands
{
    /// <summary>
    /// Represent a login command.
    /// </summary>
    public class ShowLoginCommand : ICommand
    {
        private readonly MainViewModel _viewModel;
        private readonly DialogViewModel _dialogViewModel;

        public ShowLoginCommand()
        {
            _viewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            _viewModel.PropertyChanged += (sender, e) =>
            {
                CanExecuteChanged?.Invoke(sender, e);
            };
            _dialogViewModel = ServiceLocator.Current.GetInstance<DialogViewModel>();
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return null != _viewModel.PortalConnection && null == _viewModel.PortalConnection.TokenCredential;
        }

        public async void Execute(object parameter)
        {
            var loginData = await _dialogViewModel.Coordinator.ShowLoginAsync(_viewModel, @"Login into Portal", @"Enter your credentials:");

            // Create the credential
            var credential = await IdentityManager.Current.GenerateCredentialAsync(_viewModel.PortalConnection.Portal.Uri.AbsoluteUri, loginData.Username, loginData.Password);

            // Add the credential
            IdentityManager.Current.AddCredential(credential);

            // Access the portal
            var portal = await ArcGISPortal.CreateAsync();
            _viewModel.PortalConnection = new PortalConnection(credential, portal);
        }
    }
}
