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

using AgoApp.Commands;
using AgoApp.Data;
using AgoApp.Model;
using Esri.ArcGISRuntime.Portal;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
    public class MainViewModel : ViewModelBase, IMainViewModel, IPortalConnectionDataService
    {
        private MetroWindow _mainWindow;
        private PortalConnection _portalConnection;

        private readonly ObservableCollection<ArcGISPortalItem> _basemapItems;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _basemapItems = new ObservableCollection<ArcGISPortalItem>();

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

        public ICommand LoginCommand
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginCommand>();
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LogoutCommand>();
            }
        }

        public PortalConnection PortalConnection
        {
            get { return _portalConnection; }
            set
            {
                Set(ref _portalConnection, value);

                // Query all the items and set those using the current thread
                var basemapDataService = ServiceLocator.Current.GetInstance<IBasemapDataService>();
                basemapDataService.GetBasemaps().ContinueWith((queryTask) =>
                {
                    _basemapItems.Clear();
                    foreach(var basemapItem in queryTask.Result)
                    {
                        _basemapItems.Add(basemapItem);
                    }
                }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public PortalConnection GetPortalConnection()
        {
            return _portalConnection;
        }

        public ObservableCollection<ArcGISPortalItem> BasemapItems
        {
            get { return _basemapItems; }
        }
    }
}