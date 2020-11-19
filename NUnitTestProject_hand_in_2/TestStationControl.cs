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
    [TestFixture]
    public class Tests
    {

        private IDoor _fakeDoor;
        private IRFIDReader _fakeRfidReader;
       // private IChargeControl _fakeChargerControl;
        private IDisplay _fakeDisplay;
        private UsbChargerSimulator _fakeUSBChargerSimulator; //Den har ikke en interface, så derfor kalder vi selve UsbChargerSimulatoren

        private ChargeControl _ChargeControl;
        private StationControl _uutStationControl;
        


        [SetUp]
        public void Setup()
        {

            _fakeUSBChargerSimulator = Substitute.For<UsbChargerSimulator>();
            _fakeRfidReader = Substitute.For<RFIDReader>();
            _fakeDoor = Substitute.For<Door>();
            _fakeDisplay = Substitute.For<Display>();
            _ChargeControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);
            _uutStationControl = new StationControl(_fakeDoor, _fakeRfidReader, _ChargeControl);

            
            
        }



        //OnDoorOpenTest
        #region
        [Test]
        public void TestingDoorOpen()
        {
            // Arrange - For at få det aktuelle state
           // StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);


            // Act
            _fakeDoor.OnDoorOpen();

            // Assert
            var expected = Convert.ToDouble(2); //LadeskabsState.DoorOpen
            var actual = Convert.ToDouble(_uutStationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is: Open");
        }

        [Test]
        public void TestingDoorOpenFail()
        {
            // Arrange - For at få det aktuelle state
           // StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            //Act
            //Intentional(mening) No code - Expecting to be open, but door is available

            // Assert
            var expected = Convert.ToDouble(0); //LadeskabsState.Available
            var actual = Convert.ToDouble(_uutStationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is: Available");
        }

        [Test]
        public void TestingDoorOpenLocked()
        {
            // Arrange - For at få det aktuelle state
           // StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            //Act
            _fakeDoor.OnDoorClose();
            _fakeDoor.LockDoor();
            _uutStationControl._state = StationControl.LadeskabState.Locked;
            _fakeDoor.OnDoorOpen();

            // Assert
            var expected = Convert.ToDouble(1); //LadeskabsState.Locked
            var actual = Convert.ToDouble(_uutStationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is locked");
        }
        #endregion

        //OnDoorCloseTest
        #region
        [Test]
        public void TestingDoorClosed()
        {
            // Arrange - For at få det aktuelle state
           // StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            // Act
            _uutStationControl._state = StationControl.LadeskabState.DoorOpen;
            _fakeDoor.OnDoorClose();//OnDoorClose skulle gerne ændre til ladeskabState.available


            // Assert
            var expected = Convert.ToDouble(0); //LadeskabsState.DoorClose
            var actual = Convert.ToDouble(_uutStationControl._state);

            Assert.AreEqual(expected, actual, "State of StationControl is: ladeskabState.available");
        }

        [Test]
        public void TestingDoorCloseFail()
        {
            //Arrange - For at få det aktuelle state
            //StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            //Act
            //Intentional(mening) No code - Expecting to be close, but door is available

            // Assert
            var expected = Convert.ToDouble(0); //LadeskabsState.Available
            var actual = Convert.ToDouble(_uutStationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is: Available");
        }

        [Test]
        public void TestingDoorCloseLocked()
        {
            //Arrange - For at få det aktuelle state

            //Act
            //_fakeDoor.OnDoorOpen();
           // _fakeDoor.OnDoorClose();
            //_ChargeControl.IsConnected();
            _ChargeControl.Connected.Equals(true);
            _fakeRfidReader.OnRfidRead(123);

            // Assert
            var expected = Convert.ToDouble(1); //LadeskabsState.Locked
            var actual = Convert.ToDouble(_uutStationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is locked");
        }


        #endregion

        //HandleRfidDetectedTest
        #region
        [Test]
        public void TestingReadWrongRFID()
        {
            _ChargeControl.Connected.Equals(true);
            _fakeRfidReader.OnRfidRead(13);
            _fakeRfidReader.OnRfidRead(23);

            var expected = Convert.ToDouble(13); 
            var actual = Convert.ToDouble(_uutStationControl._oldId);

            Assert.AreEqual(expected, actual, "RFID remains 13");
        }


        #endregion

    }
}
