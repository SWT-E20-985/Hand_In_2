using System;
using System.Runtime.CompilerServices;
using Hand_In_2;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace NUnitTestProject_hand_in_2
{
    [TestFixture]
    public class Tests
    {
        private IChargeControl _fakeIChargeControl;
        private ChargeControl _fakeChargeControl;
        private IUsbCharger _fakeIUsbCharger;

        private IDisplay _fakeIDisplay;
        private Display _fakeDisplay;

        private IDoor _fakeIDoor;
        private Door _fakeDoor;

        private IRFIDReader _fakeIRfidReader;
        private RFIDReader _fakeRfidReader;


        [SetUp]
        public void Setup()
        {
            _fakeIDoor = Substitute.For<IDoor>();
            _fakeIUsbCharger = Substitute.For<IUsbCharger>();

            _fakeDoor = Substitute.For<Door>();
            _fakeRfidReader = Substitute.For<RFIDReader>();
            _fakeIRfidReader = Substitute.For<IRFIDReader>();

        }



        [Test]
        public void TestingFakeDoor()
        {
            _fakeDoor.Received(1).OnDoorOpen();
            _fakeDoor.Received(1).OnDoorClose();
            _fakeDoor.Received(1).LockDoor();
            _fakeDoor.Received(1).UnlockDoor();
            //Assert.Pass();
        }

        [Test]
        public void TestingFakeRfidReader()
        {
            _fakeRfidReader.Received(1).OnRfidRead(123);
        }
    }
}