using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace Hand_In_2
{
    public interface IChargeControl
    {
        //IUsbCharger Charger { get; set; }

        bool IsConnected();
        public void StartCharge();

        public void StopCharge();

    }
}
