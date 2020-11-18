using System;
using System.Collections.Generic;
using System.Text;

namespace Hand_In_2
{
    public class Door: IDoor
    {

        protected bool Lock = false;

        public event EventHandler<ClosedEventArgs> ClosedEvent;
        public event EventHandler<OpenEventArgs> OpenEvent;


        public void OnDoorClose()
        {
            if (Lock == true)
            {
                Console.WriteLine("Skabet er i brug");
                return;
            }
            else
            {
                Console.WriteLine("skab lukket");
                ClosedEvent?.Invoke(this, new ClosedEventArgs { Closed = true });
                //OpenEvent?.Invoke(this, new OpenEventArgs { Opened = false });
            }
        }



        public void OnDoorOpen()
        {
            if (Lock == true)
            {
                Console.WriteLine("Skabet er i brug");
                return;
            }
            else
            {
                
                OpenEvent?.Invoke(this, new OpenEventArgs { Opened = true });
                //ClosedEvent?.Invoke(this, new ClosedEventArgs { Closed = false });

                Console.WriteLine("skab åbnet");
            }


        }

        public void LockDoor() 
        {
            Lock = true;
        }

        public void UnlockDoor() 
        {
            Lock = false;
        }

    }
}
