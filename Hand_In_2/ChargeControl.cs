using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks.Dataflow;
using UsbSimulator;


namespace Hand_In_2
{
    public class ChargeControl
    {

        //branch test

        private enum ChargeState
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


      


        private ChargeState _state;

        UsbChargerSimulator _UsbCharger = new UsbChargerSimulator();
       

        public ChargeControl(IUsbCharger UsbCharger) 
        {
            UsbCharger.CurrentValueEvent += CurrentHandler;
            

        }



        private void CurrentDetected() 
        {

          
            
            switch (_state)
            {
                case ChargeState.Charging:
                    IsConnected();
                    StartCharge();
                    Console.WriteLine("tlf. oplader");
                    break;

                case ChargeState.Charged:
                    IsConnected();
                    StopCharge();
                    Console.WriteLine("tlf. er opladt");
                    break;

                case ChargeState.Overload:
                    IsConnected();
                    StopCharge();
                    Console.WriteLine("fejl... for stor ladestrøm");
                    break;

                case ChargeState.NotConnected:

                    //do nothing

                    break;

            


            }
        
        
        }



        private void CurrentHandler(object sender, CurrentEventArgs e) 
        {

         
            _state = ChargeState.NotConnected;

            if (e.Current == 0)
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
            else if (e.Current > 0 && e.Current <= 5)
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
            else if(e.Current > 5 && e.Current <= 500) 
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



        public bool IsConnected()
        {
            return _UsbCharger.Connected;
        }




        public void StartCharge()
        {
         
                _UsbCharger.StartCharge();

         
        }


        public void StopCharge() 
        {
            _UsbCharger.StopCharge();
  
        }


    }
}
