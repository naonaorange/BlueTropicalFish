using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueTropicalFish.ViewModels
{
	public class DeviceDetailPageViewModel : ViewModelBase
	{
        private string debug;
        public string Debug
        {
            get { return debug; }
            set { SetProperty(ref debug, value); }
        }
        public DeviceDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Debug = (string)parameters["device"];
        }
    }
}
