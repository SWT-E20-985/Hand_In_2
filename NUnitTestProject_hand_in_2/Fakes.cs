using Hand_In_2;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject_hand_in_2
{
    internal class fakeDoor : IDoor
    {
        public int temp { get; set; }
        public event EventHandler<OpenEventArgs> OpenEvent;
        public event EventHandler<ClosedEventArgs> ClosedEvent;

        public void LockDoor()
        {
            
        }

        public void UnlockDoor()
        {
            
        }
    }


    internal class fakeIRFIDReader : IRFIDReader
    {



    }
}
