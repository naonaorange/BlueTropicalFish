using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueTropicalFish.ViewModels
{
	public class FilteringPageViewModel : ViewModelBase
	{
        public DelegateCommand ResetCommand { get; set; }

        private DeviceViewModel filteringParam;
        public DeviceViewModel FilteringParam
        {
            get { return filteringParam; }
            set { SetProperty(ref filteringParam, value); }
        }

        private string debugString;
        public string DebugString
        {
            get { return debugString; }
            set { SetProperty(ref debugString, value); }
        }

        public FilteringPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ResetCommand = new DelegateCommand(Reset);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
             FilteringParam = (DeviceViewModel)parameters["filteringParam"];
        }

        public void Reset()
        {
            FilteringParam.SetDefaultValue();
        }
	}
}
