using System;
using System.Collections.Generic;
using System.Text;


namespace Hand_In_2
{
    class RFIDReader : IRFIDReader
    {

        public event EventHandler<DetectedEventArgs> DetectedEvent;



        public void OnRfidRead(int NewRfid)
        {
           
            DetectedEvent?.Invoke(this, new DetectedEventArgs { RFID = NewRfid });
        
        }

      




    }
}
