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
        public IDisposable scan;
        public DelegateCommand ScanCommand { get; set; }
        public ObservableCollection<DeviceViewModel> ScanedDevices { get; set; }

        private bool isScanning = false;
        public bool IsScanning
        {
            get { return isScanning; }
            set { SetProperty(ref isScanning, value); }
        }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Scan Page";
            ScanCommand = new DelegateCommand(Scan);
            ScanedDevices = new ObservableCollection<DeviceViewModel>();

            this.Scan();
        }

        public Command<DeviceViewModel> ItemSelectedCommand =>
        new Command<DeviceViewModel>(device =>
        {
            var parameter = new NavigationParameters();
            parameter.Add("device", device.Name);
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
                        AddDevice(result);
                    });
                });
            }
        }

        public void AddDevice(IScanResult result)
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

            if(isExited == false)
            {
                var d = new DeviceViewModel(result);
                ScanedDevices.Add(d);
            }
        }
    }
}
