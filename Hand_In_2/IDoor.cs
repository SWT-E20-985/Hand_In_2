using System;
using System.Collections.Generic;
using System.Text;

namespace Hand_In_2
{

    public class OpenEventArgs : EventArgs
    {
        public bool Opened { get; set; }
    }

    public class ClosedEventArgs : EventArgs
    {
        public bool Closed { get; set; }
    }






    public interface IDoor
    {


        event EventHandler<ClosedEventArgs> ClosedEvent;

        event EventHandler<OpenEventArgs> OpenEvent;


        void LockDoor();


        void UnlockDoor();





    }
}
