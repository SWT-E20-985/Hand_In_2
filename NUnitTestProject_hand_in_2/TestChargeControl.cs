using Hand_In_2;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;
using System.Threading;

namespace NUnitTestProject_hand_in_2
{
   
        [TestFixture]
        public class TestChargeControl
        {
            private IDoor _fakeDoor;
            private IRFIDReader _fakeRfidReader;
         //   private IChargeControl _fakeChargeControl;
            private IDisplay _fakeDisplay;
            private UsbChargerSimulator _fakeUSBChargerSimulator; //Den har ikke en interface, så derfor kalder vi selve UsbChargerSimulatoren
            private StationControl _fakeStationControl;
                                                    
            //private ChargeControl _uutChargeControl;
           // private StationControl _StationControl;

            [SetUp]
            public void Setup()
            {

                _fakeUSBChargerSimulator = Substitute.For<UsbChargerSimulator>();
                _fakeRfidReader = Substitute.For<RFIDReader>();
                _fakeDoor = Substitute.For<Door>();
                _fakeDisplay = Substitute.For<Display>();
               // _fakeChargeControl = Substitute.For<ChargeControl>();
                _fakeStationControl = Substitute.For<StationControl>();
                
               // ChargeControl _uutChargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);
              //  StationControl _StationControl = new StationControl(_fakeDoor, _fakeRfidReader, _uutChargeControl);
            }


            [Test]
            public void TestChargeControlStateCharging()
            {
            //Arrange
            ChargeControl _uutChargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);

            //Act
            //chargeControl.CurrentDetected();
            _fakeRfidReader.OnRfidRead(123);

                //Assert
                var expected = Convert.ToDouble(0); //ChargeState.Charging
                var actuel = Convert.ToDouble(_uutChargeControl._state);
                Assert.AreEqual(expected, actuel);

            }

            [Test]
            public void TestChargeControlStateCharged()
            {
            //Arrange
            ChargeControl _uutChargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);


            //Act
            _fakeRfidReader.OnRfidRead(123); // Skab låses med RFID: 123 

            //_fakeUSBChargerSimulator.StopCharge();

            //_fakeRfidReader.OnRfidRead(123); // Skab åbnes med RFID: 123 
            _uutChargeControl.currentx = 0;


                //Assert
                var expected = Convert.ToDouble(1); //ChargeState.NotConnected
                var actuel = Convert.ToDouble(_uutChargeControl.Connected);
                Assert.AreEqual(expected, actuel);

            }

            [Test]
            public void TestChargeControlStateNotAuthorized()
            {
            //Arrange
            ChargeControl _uutChargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);                //StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, chargeControl);

            //Act
            _fakeRfidReader.OnRfidRead(123); // Skab låses med RFID: 123 

                _fakeRfidReader.OnRfidRead(321); // Skab forsøges åbnet med forkert RFID: 321 

                //Assert
                var expected = Convert.ToDouble(0); //ChargeState.Charging
                var actuel = Convert.ToDouble(_uutChargeControl._state);
                Assert.AreEqual(expected, actuel);

            }

            [Test]
            public void ChargeControlTestOverload()
            { 
                ChargeControl _uutChargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);
                _fakeRfidReader.OnRfidRead(123);
                _fakeUSBChargerSimulator.SimulateOverload(true);

                var expected = Convert.ToDouble(3); //ChargeState.Charging
                var actuel = Convert.ToDouble(_uutChargeControl._state);

            }






            //[Test]
                //public void TestChargeControlStateOverload()
                //{
                //    //Arrange
                //    ChargeControl chargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);
                //    StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, chargeControl);

                //    //Act
                //    _fakeRfidReader.OnRfidRead(123); // Skab låses med RFID: 123 
                //    _fakeRfidReader.OnRfidRead(123); // Skab forsøges åbnet med forkert RFID: 321 

                //    //Assert
                //    var expected = Convert.ToDouble(0); //ChargeState.Charging
                //    var actuel = Convert.ToDouble(chargeControl._state);
                //    Assert.AreEqual(expected, actuel);

                //}

            }
    
}
