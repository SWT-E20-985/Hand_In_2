﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks.Dataflow;
using UsbSimulator;


namespace Hand_In_2
{
    public class ChargeControl: IChargeControl
    {

        //commit til master branch
        //commit til master branch #2

        public enum ChargeState
        {
            Charging,
            Charged,
            NotConnected,
            Overload,
        };


        bool chargestate1=true;
        bool chargestate2=true;
        bool chargestate3=true;
        bool chargestate4=true;

        public double currentx = 0;
        private IDisplay _DisplayCC;

        public bool Connected{ get;  private set; }

        //isConnected skal sende enten false eller true tilbage til stationcontrol om at den er connected ie. true == _charger.Connected


        public ChargeState _state;

        IUsbCharger _UsbCharger;

        public ChargeControl(IUsbCharger UsbCharger, IDisplay DisplayCC) 
        {
            UsbCharger.CurrentValueEvent += CurrentHandler;

            _UsbCharger = UsbCharger;

            _DisplayCC = DisplayCC;


            if (_UsbCharger.Connected == true)
            {
                _DisplayCC.print("telefon er tilsluttet");
                IsConnected();
            }
            else
            {
                _DisplayCC.print("telefon er ikke tilsluttet");
                return;
            }

        }

        public ChargeControl()
        {
        }


        public void CurrentDetected() 
        {
            
            //if (n != null) _state = ChargeState; 
            switch (_state)
            {
                case ChargeState.Charging:
                    _DisplayCC.print("tlf. oplader");
                    break;

                case ChargeState.Charged:
                   StopCharge();
                    _DisplayCC.print("tlf. er opladt");
                    break;

                case ChargeState.Overload:
                 
                    StopCharge();
                    _DisplayCC.print("fejl... for stor ladestrøm");
                    break;

                case ChargeState.NotConnected:

                    _DisplayCC.print("ingen forbindelse");
                    Connected = false;
                    //do nothing

                    break;
            }
        }



        public void CurrentHandler(object sender, CurrentEventArgs e)
        {

            currentx = e.Current;
          _state = ChargeState.NotConnected;

            //_DisplayCC.CurrentCharge(e.Current);

            if (currentx == 0)
            {
                _state = ChargeState.NotConnected;

                if (chargestate1 == true) 
                {
                    CurrentDetected();
                    chargestate1 = false;
                    chargestate2 = true;
                    chargestate3 = true;
                    chargestate4 = true;
                }
            }
            else if (currentx > 0 && currentx <= 5)
            {
                _state = ChargeState.Charged;
                
                if (chargestate2 == true)
                {
                   CurrentDetected();
                    chargestate1 = true;
                    chargestate2 = false;
                    chargestate3 = true;
                    chargestate4 = true;
                }


            }
            else if(currentx > 5 && currentx <= 500) 
            {

               
                _state = ChargeState.Charging;

                

                if (chargestate3 == true)
                {
                   CurrentDetected();
                    chargestate1 = true;
                    chargestate2 = true;
                    chargestate3 = false;
                    chargestate4 = true;

                }



            }
            else
            {
                _state = ChargeState.Overload;

                _DisplayCC.print("OVERLOAD");

                if (chargestate4 == true)
                {
                   CurrentDetected();

                    chargestate1 = true;
                    chargestate2 = true;
                    chargestate3 = true;
                    chargestate4 = false;

                }

            }


        }



        public void IsConnected()
        {
            Connected = true;
        }




        public void StartCharge()
        {
            _UsbCharger.StartCharge();
            return;
        }


        public void StopCharge()
        {
            _UsbCharger.StopCharge();
           return;
        }

    }
}
