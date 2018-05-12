using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Prism.Mvvm;
using Plugin.BluetoothLE;

namespace BlueTropicalFish.ViewModels
{
    public class DeviceViewModel: BindableBase
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

        private string status;
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        public void SetStatus(ConnectionStatus s)
        {
            if (s == ConnectionStatus.Disconnected) Status = "Disconnected";
            else if (s == ConnectionStatus.Disconnecting) Status = "Disconnecting";
            else if (s == ConnectionStatus.Connected) Status = "Connected";
            else Status = "Connecting";
        }

        private string localName;
        public string LocalName
        {
            get { return localName; }
            set { SetProperty(ref localName, value); }
        }

        private string connectable;
        public string Connectable
        {
            get { return connectable; }
            set { SetProperty(ref connectable, value); }
        }

        public void SetConnectable(bool b)
        {
            if (b == true) Connectable = "Connectable";
            else Connectable = "Disconnectable";
        }

        public DeviceViewModel(IScanResult result)
        {
            this.Img = ImageSource.FromFile("bt.png");
            this.Update(result);
        }

        public void Update(IScanResult result)
        {
            this.Name = result.Device.Name;
            this.Uuid = result.Device.Uuid.ToString();
            this.Rssi = result.Rssi;
            this.SetStatus(result.Device.Status);
            this.LocalName = result.AdvertisementData.LocalName;
            this.SetConnectable(result.AdvertisementData.IsConnectable);
        }
    }
}
