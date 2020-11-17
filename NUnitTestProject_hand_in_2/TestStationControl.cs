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

        private FakeDoor _fakeDoor;
        private FakeRFIDReader _fakeRfidReader;
        private UsbChargerSimulator _fakeUSBCharger;


        [SetUp]
        public void Setup()
        {

            _fakeUSBCharger = Substitute.For<UsbChargerSimulator>(); // Ikke nødvendigt at lave en FakeUsbChargerSimulator der testen er udleveret
            _fakeRfidReader = Substitute.For<FakeRFIDReader>();
            _fakeDoor = Substitute.For<FakeDoor>();

        }


        [Test]
        public void TestingDoorOpen()
        {
            // Arrange 
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeUSBCharger);

            // Act
            _fakeDoor.OnDoorOpen();

            // Assert
            var expected = Convert.ToDouble(2); //LadeskabsState.DoorOpen
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is: Open");
        }

        [Test]
        public void TestingDoorFail()
        {
            //Arrange
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeUSBCharger);

            //Act
            //Intentional(mening) No code - Expecting to be open, but door is closed

            // Assert
            var expected = Convert.ToDouble(2); //LadeskabsState.DoorOpen
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol fail");
        }

        [Test]
        public void TestingDoorClosed()
        {
            //Arrange
            StationControl stationControl = new StationControl(_fakeDoor, _fakeRfidReader, _fakeUSBCharger);

            //Act
            _fakeDoor.OnDoorOpen();
            _fakeDoor.OnDoorClose();

            // Assert
            var expected = Convert.ToDouble(1); //LadeskabsState.DoorOpen
            var actual = Convert.ToDouble(stationControl._state);

            Assert.AreEqual(expected, actual, "State of stationcontrol is closed");
        }


    }
}
