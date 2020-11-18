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
        private IChargeControl _fakeChargerControl;
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
            _fakeChargerControl = Substitute.For<ChargeControl>();
            _fakeStationControl = Substitute.For<StationControl>();

            //ChargeControl _fakeChargerControl = new ChargeControl(_fakeUSBChargerSimulator, _fakeDisplay);
            
        }



        //OnDoorOpenTest
        #region
        [Test]
        public void TestingDoorOpen()
        {
            // Arrange - For at få det aktuelle state
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);


            // Act
            _fakeDoor.OnDoorOpen();

            // Assert
            var expected = Convert.ToDouble(2); //LadeskabsState.DoorOpen
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is: Open");
        }

        [Test]
        public void TestingDoorOpenFail()
        {
            // Arrange - For at få det aktuelle state
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            //Act
            //Intentional(mening) No code - Expecting to be open, but door is available

            // Assert
            var expected = Convert.ToDouble(0); //LadeskabsState.Available
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is: Available");
        }

        [Test]
        public void TestingDoorOpenLocked()
        {
            // Arrange - For at få det aktuelle state
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            //Act
            _fakeDoor.OnDoorOpen();
            _fakeDoor.OnDoorClose();

            // Assert
            var expected = Convert.ToDouble(1); //LadeskabsState.Locked
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is locked");
        }
        #endregion

        //OnDoorCloseTest
        #region
        [Test]
        public void TestingDoorClose()
        {
            // Arrange - For at få det aktuelle state
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            // Act
            _fakeDoor.OnDoorClose();

            // Assert
            var expected = Convert.ToDouble(1); //LadeskabsState.DoorClose
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is: Close");
        }

        [Test]
        public void TestingDoorCloseFail()
        {
            //Arrange - For at få det aktuelle state
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            //Act
            //Intentional(mening) No code - Expecting to be close, but door is available

            // Assert
            var expected = Convert.ToDouble(0); //LadeskabsState.Available
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is: Available");
        }

        [Test]
        public void TestingDoorCloseLocked()
        {
            //Arrange - For at få det aktuelle state
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeChargerControl);

            //Act
            _fakeDoor.OnDoorOpen();
            _fakeDoor.OnDoorClose();

            // Assert
            var expected = Convert.ToDouble(1); //LadeskabsState.Locked
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is locked");
        }


        #endregion

        //HandleRfidDetectedTest
        #region
        public void HandleRfidDetectedTest()
        {


        }

        #endregion

    }
}
