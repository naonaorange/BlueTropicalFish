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


namespace BlueTropicalFish.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand ScanCommand { get; set; }

        public ObservableCollection<PeripheralDevice> ScanedDevices { get; set; }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Scan Page";
            ScanCommand = new DelegateCommand(Scan);
            ScanedDevices = new ObservableCollection<PeripheralDevice>();
        }

        public void Scan()
        {
            CrossBleAdapter.Current.Scan().Subscribe(result =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    AddDevice(result);
                });
            });
        }

        public void AddDevice(IScanResult result)
        {
            bool isExited = false;
            int index = 0;

            foreach(var device in ScanedDevices)
            {
                if (device.Uuid == result.Device.Uuid.ToString())
                {
                    isExited = true;
                    ScanedDevices.RemoveAt(index);

                    var d = new PeripheralDevice();
                    d.Name = result.Device.Name;
                    d.Uuid = result.Device.Uuid.ToString();
                    d.Rssi = "RSSI: " + result.Rssi.ToString();
                    ScanedDevices.Add(d);

                    break;
                }
                index++;
            }

            if(isExited == false)
            {
                var d = new PeripheralDevice();
                d.Name = result.Device.Name;
                d.Uuid = result.Device.Uuid.ToString();
                d.Rssi = "RSSI: " + result.Rssi.ToString();
                
                ScanedDevices.Add(d);
            }
        }
    }


    public class PeripheralDevice
    {
        public string Name { get; set; }
        public string Uuid { get; set; }
        public string Rssi { get; set; }
        public ImageSource Img { get; set; }

        public PeripheralDevice()
        {
            Img = ImageSource.FromFile("bt.png");
        }
    }
}
