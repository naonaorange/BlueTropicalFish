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
using BlueTropicalFish.Models;


namespace BlueTropicalFish.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public IDisposable scan;
        public DelegateCommand ScanCommand { get; set; }
        public ObservableCollection<PeripheralDevice> ScanedDevices { get; set; }

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
            ScanedDevices = new ObservableCollection<PeripheralDevice>();
        }

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
                    ScanedDevices.RemoveAt(index);

                    var d = new PeripheralDevice(result);
                    ScanedDevices.Insert(index, d);
                    break;
                }
                index++;
            }

            if(isExited == false)
            {
                var d = new PeripheralDevice(result);
                ScanedDevices.Add(d);
            }
        }
    }
}
