using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace BlueTropicalFish.ViewModels
{
	public class DeviceDetailPageViewModel : ViewModelBase
	{
        private DeviceViewModel device;
        public DeviceViewModel Device
        {
            get { return device; }
            set { SetProperty(ref device, value); }
        }

        public DeviceDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Device = (DeviceViewModel)parameters["device"];
        }
    }
}
