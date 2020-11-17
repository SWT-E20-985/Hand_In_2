using Hand_In_2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available, //dør lukket?
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        public LadeskabState _state;
        private IUsbCharger _charger;
        private IDoor _door;
        private int _oldId;

        public bool OpenDoor{ get; set; }
        public bool CloseDoor { get; set; }
        public int CurrentRFid { get; set; }
        

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor

        public StationControl(IDoor door, IRFIDReader read,IUsbCharger charger)
        {
           _charger = charger;

            _door = door;
            door.ClosedEvent += DoorClosed;
            door.OpenEvent += DoorOpened;
            read.DetectedEvent += HandleRfidDetected;
        }



        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere


        private void HandleRfidDetected(object sender, DetectedEventArgs e) 
        {
            CurrentRFid = e.RFID;
            RfidDetected(CurrentRFid);

            Console.WriteLine("station control modtaget RFID");
        }

        private void DoorOpened(object sender, OpenEventArgs e)
        {
            OpenDoor = e.Opened;
            //case ladeskabstate switch til open i RfidDetected(int id)
            _state = LadeskabState.DoorOpen;

            Console.WriteLine("station control modtaget skab åbnet");
        }



        private void DoorClosed(object sender, ClosedEventArgs e)
        {

            CloseDoor = e.Closed;
            //case ladeskabstate switch til available i RfidDetected(int id)
            _state = LadeskabState.Locked;

            Console.WriteLine("station control modtaget skab lukket");
        }





    }
}
