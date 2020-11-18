using Hand_In_2;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace NUnitTestProject_hand_in_2
{
    class TestChargeControl
    {
        [TestFixture]
        public class Tests
        {

            private IDoor _fakeDoor;
            private IRFIDReader _fakeRfidReader;
            private IChargeControl _fakeChargeControl;
            private IDisplay _fakeDisplay;

            private StationControl _fakeStationControl;
            private UsbChargerSimulator _fakeUSBChargerSimulator; //Den har ikke en interface, så derfor kalder vi selve UsbChargerSimulatoren


            [SetUp]
            public void Setup()
            {

                _fakeUSBChargerSimulator = Substitute.For<UsbChargerSimulator>();
                _fakeRfidReader = Substitute.For<RFIDReader>();
                _fakeDoor = Substitute.For<Door>();
                _fakeDisplay = Substitute.For<Display>();
                _fakeChargeControl = Substitute.For<ChargeControl>();
                _fakeStationControl = Substitute.For<StationControl>();

                //ChargeControl _fakeChargerControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);

            }


            [Test]
            public void TestChargeControlStateCharging()
            {
                //Arrange
                ChargeControl chargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);

                //Act
                //chargeControl.CurrentDetected();
                _fakeRfidReader.OnRfidRead(123);

                //Assert
                var expected = Convert.ToDouble(0); //ChargeState.Charging
                var actuel = Convert.ToDouble(chargeControl._state);
                Assert.AreEqual(expected, actuel);

            }

            [Test]
            public void TestChargeControlStateNotCharging()
            {
                //Arrange
                ChargeControl chargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);
                // StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, chargeControl);


                //Act
                _fakeRfidReader.OnRfidRead(123); // Skab låses med RFID: 123 

                _fakeRfidReader.OnRfidRead(123); // Skab åbnes med RFID: 123 

                //Assert
                var expected = Convert.ToDouble(2); //ChargeState.NotConnected
                var actuel = Convert.ToDouble(chargeControl._state);
                Assert.AreEqual(expected, actuel);

            }

            [Test]
            public void TestChargeControlStateNotAuthorized()
            {
                //Arrange
                ChargeControl chargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);
                StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, chargeControl);

                //Act
                _fakeRfidReader.OnRfidRead(123); // Skab låses med RFID: 123 

                _fakeRfidReader.OnRfidRead(321); // Skab forsøges åbnet med forkert RFID: 321 

                //Assert
                var expected = Convert.ToDouble(0); //ChargeState.Charging
                var actuel = Convert.ToDouble(chargeControl._state);
                Assert.AreEqual(expected, actuel);

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
}
