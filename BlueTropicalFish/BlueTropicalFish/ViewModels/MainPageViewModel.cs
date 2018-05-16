using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.BluetoothLE;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BlueTropicalFish.Views;

namespace BlueTropicalFish.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand ScanCommand { get; set; }
        public DelegateCommand DebCommand { get; set; }
        public DelegateCommand EnableFilteringCommand { get; set; }
        public DelegateCommand SetFilteringParamCommand { get; set; }
        

        public IDisposable scan;
        public ObservableCollection<DeviceViewModel> ScanedDevices { get; set; }
        private DeviceViewModel filteringParam;

        private bool isScanning = false;
        public bool IsScanning
        {
            get { return isScanning; }
            set { SetProperty(ref isScanning, value); }
        }

        private bool isFiltering = false;
        public bool IsFiltering
        {
            get { return isFiltering; }
            set { SetProperty(ref isFiltering, value); }
        }


        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Scan Page";
            ScanCommand = new DelegateCommand(Scan);
            EnableFilteringCommand = new DelegateCommand(EnableFiltering);
            SetFilteringParamCommand = new DelegateCommand(SetFilteringParam);

            ScanedDevices = new ObservableCollection<DeviceViewModel>();
            filteringParam = new DeviceViewModel();

            this.Scan();
        }

        public Command<DeviceViewModel> ItemSelectedCommand =>
        new Command<DeviceViewModel>(device =>
        {
            var parameter = new NavigationParameters();
            parameter.Add("device", device);
            base.NavigationService.NavigateAsync(nameof(DeviceDetailPage), parameter);
        });

        public void Scan()
        {
            if(IsScanning == true)
            {
                scan?.Dispose();
                IsScanning = false;
            }
            else
            {
                ScanedDevices.Clear();
                IsScanning = true;

                scan = CrossBleAdapter.Current.Scan().Subscribe(result =>
                {
                    Device.BeginInvokeOnMainThread(() => {
                        this.AddDevice(result);
                    });
                });
            }
        }

        private void AddDevice(IScanResult result)
        {
            bool isExited = false;
            int index = 0;

            foreach(var device in ScanedDevices)
            {
                if (device.Uuid.Equals(result.Device.Uuid.ToString()))
                {
                    isExited = true;
                    device.Update(result);
                    break;
                }
                index++;
            }

            if (isExited == false)
            {
                if (IsFiltering == false)
                {
                    var d = new DeviceViewModel(result);
                    ScanedDevices.Add(d);
                }
                else
                {
                    var d = new DeviceViewModel(result);
                    if (this.IsMeetFilteringCondition(d, filteringParam))
                    {
                        ScanedDevices.Add(d);
                    }
                }
            }

            Title = filteringParam.Name;
        }

        public void EnableFiltering()
        {
            if (IsFiltering)
            {
                int index = 0;
                var deleteIndex = new List<int>();

                foreach(var device in ScanedDevices)
                {
                    if(!this.IsMeetFilteringCondition(device, filteringParam))
                    {
                        deleteIndex.Add(index);
                    }

                    index++;
                }

                deleteIndex.Reverse();
                foreach(var i in deleteIndex)
                {
                    ScanedDevices.RemoveAt(i);
                }
            }
        }

        public void SetFilteringParam()
        {
            var parameter = new NavigationParameters();
            parameter.Add("filteringParam", filteringParam);
            base.NavigationService.NavigateAsync(nameof(FilteringPage), parameter);
        }

        private bool IsMeetFilteringCondition(DeviceViewModel device, DeviceViewModel filter)
        {
            bool isFiltering = false;

            if (filter.IsEmptyData()) return true;

            if(device.Name.IndexOf(filter.Name, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                isFiltering = true;
            }

            return isFiltering;
        }
    }
}
