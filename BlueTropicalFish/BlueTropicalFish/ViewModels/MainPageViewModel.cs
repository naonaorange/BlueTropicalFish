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

        private string debugString;
        public string DebugString
        {
            get { return debugString; }
            set { SetProperty(ref debugString, value); }
        }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
            DebugString = "Debug";
            ScanCommand = new DelegateCommand(Scan);

            ScanedDevices = new ObservableCollection<PeripheralDevice>();
            var d = new PeripheralDevice();
            d.Name = "hoge";
            d.Detail = "detail";
            ScanedDevices.Add(d);
        }

        public void Scan()
        {
            CrossBleAdapter.Current.Scan().Subscribe(result =>
            {
                DebugString = result.Device.Name;
                
                /*
                Device.BeginInvokeOnMainThread(() => {
                });
                */ 
            });
        }
    }

    public class PeripheralDevice
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public ImageSource Img { get; set; }

        public PeripheralDevice()
        {
            Img = ImageSource.FromFile("bt.png");
        }
    }
}
