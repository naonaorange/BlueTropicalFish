using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Prism.Mvvm;
using Plugin.BluetoothLE;

namespace BlueTropicalFish.ViewModels
{
    public class PeripheralDeviceViewModel: BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if(value == null)
                {
                    SetProperty(ref name, "No name");
                }
                else
                {
                    SetProperty(ref name, value);
                }
            }
        }

        private string uuid;
        public string Uuid
        {
            get { return uuid; }
            set { SetProperty(ref uuid, value); }
        }

        private int rssi;
        public int Rssi
        {
            get { return rssi; }
            set { SetProperty(ref rssi, value); }
        }

        private ImageSource img;
        public ImageSource Img
        {
            get { return img; }
            set { SetProperty(ref img, value); }
        }

        public PeripheralDeviceViewModel(IScanResult result)
        {
            Img = ImageSource.FromFile("bt.png");
            Name = result.Device.Name;
            Uuid = result.Device.Uuid.ToString();
            Rssi = result.Rssi;
        }

        public void Update(IScanResult result)
        {
            this.Name = result.Device.Name;
            this.Uuid = result.Device.Uuid.ToString();
            this.Rssi = result.Rssi;
        }
    }
}
