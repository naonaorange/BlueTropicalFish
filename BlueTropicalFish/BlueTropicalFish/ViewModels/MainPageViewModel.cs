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

        private ImageSource img;
        public ImageSource Img
        {
            get { return img; }
            set { SetProperty(ref img, value); }
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
            //d.ImageUri = ImageSource.FromResource("BlueTropicalFish.Resources.bt.jpg");
            //d.ImageUri = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"));
            
            ScanedDevices.Add(d);

            d = new PeripheralDevice();
            d.Name = "fuga";
            d.Detail = "detail";
            //d.ImageUri = ImageSource.FromResource("BlueTropicalFish.Resources.bt.jpg");
            //d.ImageUri = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"));
            ScanedDevices.Add(d);
        }

        public void Scan()
        {
            var d = new PeripheralDevice();
            d.Name = "fuga";
            d.Detail = "detail";
            //d.ImageUri = ImageSource.FromResource("BlueTropicalFish.Resources.bt.jpg");
            //d.ImageUri = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"));
            ScanedDevices.Add(d);

            /*
            CrossBleAdapter.Current.Scan().Subscribe(result =>
            {
                Debug.WriteLine($"{result.Device.Name}:{result.Device.Uuid}:{result.Rssi}");
                //DebugString = result.Device.Name;
                
                Device.BeginInvokeOnMainThread(() => {
                    PeripheralDevice d = new PeripheralDevice();
                    d.Name = result.Device.Name;
                    d.Detail = "aaa";
                    d.ImageUri = ImageSource.FromResource("BlueTropicalFish.Resources.bt.jpg");
                    //d.ImageUri = "Resources/bt.jpg";
                    ScanedDevices.Add(d);
                });
                

            });
            */

        }
    }

    public class PeripheralDevice
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        //public ImageSource ImageUri { get; set; }
        //public string ImageUri { get; set; }

        

        


    }
}
