﻿using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace Hand_In_2
{
    public interface IChargeControl
    {


        void StartCharge();
        void StopCharge();

        void IsConnected();
        void CurrentDetected();
        
        //void CurrentHandler();


        bool Connected { get; }

    }
}
