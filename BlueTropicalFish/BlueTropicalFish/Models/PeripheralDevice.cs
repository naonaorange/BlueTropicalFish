using Plugin.BluetoothLE;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BlueTropicalFish.Models
{
    public class PeripheralDevice
    {
        public string Name { get; set; }
        public string Uuid { get; set; }
        public string Rssi { get; set; }
        public ImageSource Img { get; set; }

        public PeripheralDevice(IScanResult result)
        {
            Img = ImageSource.FromFile("bt.png");

            Name = result.Device.Name;
            Uuid = result.Device.Uuid.ToString();
            Rssi = "RSSI: " + result.Rssi.ToString();
        }
    }
}
