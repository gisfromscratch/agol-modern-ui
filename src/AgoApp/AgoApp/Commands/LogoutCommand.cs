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
using System;
using System.Windows.Input;

namespace AgoApp.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        public LogoutCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
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
