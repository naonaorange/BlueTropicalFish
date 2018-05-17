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
        public const string DEFAULT_NAME = "";
        private string name = DEFAULT_NAME;
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

        public const string DEFAULT_UUID = "";
        private string uuid = DEFAULT_UUID;
        public string Uuid
        {
            get { return uuid; }
            set { SetProperty(ref uuid, value); }
        }

        public const int DEFAULT_RSSI = -100;
        private int rssi = DEFAULT_RSSI;
        public int Rssi
        {
            get { return rssi; }
            set { SetProperty(ref rssi, value); }
        }

        public const string DEFAULT_STATUS = "Disconnected";
        private string status = DEFAULT_STATUS;
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

        public const string DEFAULT_LOCAL_NAME = "";
        private string localName = DEFAULT_LOCAL_NAME;
        public string LocalName
        {
            get { return localName; }
            set { SetProperty(ref localName, value); }
        }

        public const string DEFAULT_CONNECTABLE = "Disconnectable";
        private string connectable = DEFAULT_CONNECTABLE;
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

        public const bool DEFAULT_IS_FAVORITE = false;
        private bool isFavorite = DEFAULT_IS_FAVORITE;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set { SetProperty(ref isFavorite, value); }
        }

        private ImageSource img;
        public ImageSource Img
        {
            get { return img; }
            set { SetProperty(ref img, value); }
        }

        public DeviceViewModel()
        {
            Img = ImageSource.FromFile("bt.png");
            SetStatus(ConnectionStatus.Disconnected);
            SetConnectable(false);

        }

        public DeviceViewModel(IScanResult result)
        {
            Img = ImageSource.FromFile("bt.png");
            this.Update(result);
        }

        public bool IsEmptyData()
        {
            bool isEmpty = false;

            if (Name.Equals(DEFAULT_NAME))
            {
                if (Uuid.Equals(DEFAULT_UUID))
                {
                    if(Rssi == DEFAULT_RSSI)
                    {
                        if (Status.Equals(DEFAULT_STATUS))
                        {
                            if (LocalName.Equals(DEFAULT_LOCAL_NAME))
                            {
                                if (Connectable.Equals(DEFAULT_CONNECTABLE))
                                {
                                    isEmpty = true;
                                }
                            }
                        }
                    }
                }
            }
            return isEmpty;
        }

        public void SetDefaultValue()
        {
            Name = DEFAULT_NAME;
            Uuid = DEFAULT_UUID;
            Rssi = DEFAULT_RSSI;
            Status = DEFAULT_STATUS;
            LocalName = DEFAULT_LOCAL_NAME;
            Connectable = DEFAULT_CONNECTABLE;
            IsFavorite = DEFAULT_IS_FAVORITE;
        }

        public void Update(IScanResult result)
        {
            Name = result.Device.Name;
            Uuid = result.Device.Uuid.ToString();
            Rssi = result.Rssi;
            this.SetStatus(result.Device.Status);
            LocalName = result.AdvertisementData.LocalName;
            this.SetConnectable(result.AdvertisementData.IsConnectable);
        }
    }
}
