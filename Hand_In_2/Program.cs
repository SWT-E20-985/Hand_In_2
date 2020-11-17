using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using UsbSimulator;



namespace Hand_In_2
{
    class Program
    {

        
        static void Main(string[] args)
        {
            //foerste test
            // Assemble your system here from all the classes
        UsbChargerSimulator charger = new UsbChargerSimulator();   
        FakeDoor door = new FakeDoor();
        FakeRFIDReader rfidReader = new FakeRFIDReader();
        StationControl stationControl = new StationControl(door, rfidReader, charger);
        ChargeControl chargeControl = new ChargeControl(charger);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}


