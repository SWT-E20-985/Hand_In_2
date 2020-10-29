using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace Hand_In_2
{
    public class ChargeControl: IChargeControl
    {

        //IUsbCharger Charger;

        public ChargeControl() 
        {
            //Charger = new USBCharger();
        }

        public bool IsConnected() 
        {
           return true;
        }
        public void StartCharge() 
        {
            
        }

        public void StopCharge() 
        {
        
        }


    }
}
