using System;
using System.Collections.Generic;
using System.Text;

namespace Hand_In_2
{
    public class DetectedEventArgs : EventArgs
    {
        public int RFID { get; set; }
    }


    public interface IRFIDReader
    {

        void OnRfidRead(int n);
        event EventHandler<DetectedEventArgs> DetectedEvent;

    }
}
