using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BabySitterKata;

namespace BabySitterTests
{
    [TestClass]
    public class TimeConversionTests
    {
        [TestMethod]
        [DataTestMethod]
        
        [DataRow("6:30 pm")]
        [DataRow("7:45pm")]
        [DataRow("4:01AM")]
        [DataRow("12:29am")]
        
        public void TimeRoundsDownToNearestHour(string time)
        {
            //Arrange
            DateTime dt = DateTime.Parse(time);
            NightJob job = new NightJob();
            DateTime controlMinutes = DateTime.Parse("12 AM");
            
            //Act
            DateTime roundedDT = job.RoundToNearestHour(dt);
            //Assert
            Assert.AreEqual(roundedDT.Minute, controlMinutes.Minute );
            Assert.AreNotEqual(roundedDT, dt);
            Assert.IsNotNull(roundedDT);


        }
        [TestMethod]
        [DataTestMethod]
        [DataRow("6:54 pm")]
        [DataRow("7:45pm")]
        [DataRow("12:30am")]
        [DataRow("11:30pm")]
        public void TimeRoundsUpToNearestHour(string time)
        {
            //Arrange
            DateTime dt = DateTime.Parse(time);
            NightJob job = new NightJob();
            DateTime control = DateTime.Parse("12 AM");
            DateTime controlHour = dt.AddHours(1.00);
            //Act
            DateTime roundedDT = job.RoundToNearestHour(dt);
            //Assert
            Assert.AreEqual(roundedDT.Minute, control.Minute);
            Assert.AreEqual(roundedDT.Hour, controlHour.Hour);
            Assert.AreNotEqual(roundedDT, dt);
            Assert.IsNotNull(roundedDT);


        }
        [TestMethod]
        public void EvenTimeShouldReturnUnchanged()
        {
            //Arrange 
            DateTime evenTime = DateTime.Parse("5pm");
            NightJob job = new NightJob();
            //Act
            DateTime testTime = job.RoundToNearestHour(evenTime);
            //Assert
            Assert.AreEqual(evenTime, testTime);
            Assert.IsNotNull(testTime);
        }






    }
}
