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

/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AgoApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using AgoApp.Commands;
using AgoApp.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace AgoApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                //SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                // We have to use the factory type of registration
                // otherwise a duplicated key exception would be thrown here!
                // We are using the service locator to force a singleton
                SimpleIoc.Default.Register<IMainViewModel>(() =>
                    ServiceLocator.Current.GetInstance<MainViewModel>()
                );
                SimpleIoc.Default.Register<IPortalConnectionDataService>(() =>
                    ServiceLocator.Current.GetInstance<MainViewModel>()
                );
                SimpleIoc.Default.Register<IBasemapDataService, BasemapDataService>();
            }

            // We have to use the factory type of registration
            // otherwise a duplicated key exception would be thrown here!
            SimpleIoc.Default.Register(() => new MainViewModel());
            SimpleIoc.Default.Register<DialogViewModel>();

            // Registering the commands
            SimpleIoc.Default.Register<LoginCommand>();
            SimpleIoc.Default.Register<LogoutCommand>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public DialogViewModel Dialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DialogViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}