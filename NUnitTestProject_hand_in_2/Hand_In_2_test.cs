//using System;
//using System.Runtime.CompilerServices;
//using Hand_In_2;
//using NSubstitute;
//using NUnit.Framework;
//using UsbSimulator;

//namespace NUnitTestProject_hand_in_2
//{
//    [TestFixture]
//    public class Tests
//    {

//        private IUsbCharger _fakeIUsbCharger;
//        private IDoor _fakeIDoor;
//        private IRFIDReader _fakeIRfidReader;

//        [SetUp]
//        public void Setup()
//        {
//            _fakeIDoor = Substitute.For<IDoor>();
//            _fakeIUsbCharger = Substitute.For<IUsbCharger>();
//            _fakeIRfidReader = Substitute.For<IRFIDReader>();
//        }

//        [Test]
//        public void TestingFakeDoor()
//        {
//            //The functions bellow should be tested for that they are going to be called only one time
//            _fakeIDoor.Received(1).OnDoorOpen();
//            _fakeIDoor.Received(1).OnDoorClose();
//            _fakeIDoor.Received(1).LockDoor();
//            _fakeIDoor.Received(1).UnlockDoor();
//            //Assert.Pass();
//        }

//        [Test]
//        public void TestingFakeRfidReader()
//        {
//            _fakeIRfidReader.Received(1).OnRfidRead(1233);
//        }



//    }
//}