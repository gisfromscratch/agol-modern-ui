/*
 * Copyright 2016 Jan Tschada
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
            try
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
            catch (Exception ex)
            {
                await _dialogViewModel.Coordinator.ShowMessageAsync(_viewModel, @"AGO App", ex.Message);
            }
        }
    }
}
