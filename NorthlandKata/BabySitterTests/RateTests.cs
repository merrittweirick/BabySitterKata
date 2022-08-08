using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BabySitterKata;

namespace BabySitterTests
{
    [TestClass]
    public class RateTests
    {
        [DataTestMethod]
        [DataRow("5 pm", "8pm")]
        [DataRow("5pm","5pm")]
        [DataRow("5pm","12am")]
        [DataRow("5pm","4am")]
        public void RateBeforeBedTimeReturnsCorrectAmount(string start , string bed )
        {
            //Arrange
            NightJob job = new NightJob();
            DateTime startTime = job.RoundToNearestHour( DateTime.Parse(start));
            DateTime bedTime = job.RoundToNearestHour(DateTime.Parse(bed));
            TimeSpan timespan = bedTime - startTime;
            int timespanInt = Convert.ToInt32(timespan.Hours);
            double tester = timespanInt * 12;


            //Act
            double charge = job.ChargeBeforeBed(startTime, bedTime);
            //Assert
            Assert.AreEqual(charge, (timespanInt == 0 ? 0.00 : tester)); //if bedtime and startime aren't the same time charge should match tester
            Assert.IsNotNull(charge);



        }
        
        [DataTestMethod]
        [DataRow("8pm")]
        [DataRow("11:30pm")]
        [DataRow("7:29pm")]
        [DataRow("5:35pm")]
        public void RateBetweenBedAndMidnightReturnsCorrectAmount(string bedtime)
        {
            //Arrange
            NightJob job = new NightJob();
            DateTime bedTime = job.RoundToNearestHour(DateTime.Parse(bedtime));
            DateTime midnight = job.RoundToNearestHour(DateTime.Parse("12am"));
            TimeSpan timespan = bedTime - midnight;
            int timespanInt = Convert.ToInt32(timespan.Hours);
            double tester = timespanInt * 8;
            
            //Act
            double charge = job.ChargeFromBedToMidnight(bedTime, midnight);
            //Assert
            Assert.AreEqual(charge, (timespanInt == 0 ? 0.00 : tester)); 
            Assert.IsNotNull(charge);
        }
        
        [DataTestMethod]
        [DataRow("2:30am")]
        [DataRow("12am")]
        public void RateBetweenMidnightAndEndReturnsCorrectAmount(string end)
        {
            //Arrange
            NightJob job = new NightJob();
            DateTime endTime = job.RoundToNearestHour(DateTime.Parse(end));
            DateTime midnight = job.RoundToNearestHour(DateTime.Parse("12am"));
            TimeSpan timespan = endTime - midnight;
            int timespanInt = Convert.ToInt32(timespan.Hours);
            double tester = timespanInt * 8;
            //Act
            double charge = job.ChargeFromMidnightToEnd(midnight, endTime);
            //Assert
            Assert.AreEqual(charge, (timespanInt == 0 ? 0.00 : tester));
            Assert.IsNotNull(charge);
        }
        
        [DataTestMethod]
        [DataRow("5pm","8pm","4am")]
        public void NightlyChargeReturnsCorrectAmountHappy(string start, string bed, string end)
        {
            //Arrange
            NightJob job = new NightJob();
            DateTime startTime = job.RoundToNearestHour(DateTime.Parse(start));
            DateTime bedTime = job.RoundToNearestHour(DateTime.Parse(bed));
            DateTime endTime = job.RoundToNearestHour(DateTime.Parse(end));
            DateTime midnight = DateTime.Parse("12am");

            //Act
            double charge = job.NightlyCharge(startTime, bedTime, midnight, endTime);
            //Assert
            Assert.AreEqual(charge, 132.00);
        }
        [DataTestMethod]
        [DataRow("5pm", "5pm", "12am")]
        public void NightlyChargeReturnsCorrectAmountEdge1(string start, string bed, string end)
        {
            //Arrange
            NightJob job = new NightJob();
            DateTime startTime = job.RoundToNearestHour(DateTime.Parse(start));
            DateTime bedTime = job.RoundToNearestHour(DateTime.Parse(bed));
            DateTime endTime = job.RoundToNearestHour(DateTime.Parse(end));
            DateTime midnight = DateTime.Parse("12am");

            //Act
            double charge = job.NightlyCharge(startTime, bedTime, midnight, endTime);
            //Assert
            Assert.AreEqual(charge, 56.00);
        }
        [DataTestMethod]
        [DataRow("5pm", "5:30pm", "2am")]
        public void NightlyChargeReturnsCorrectAmountEdge2(string start, string bed, string end)
        {
            //Arrange
            NightJob job = new NightJob();
            DateTime startTime = job.RoundToNearestHour(DateTime.Parse(start));
            DateTime bedTime = job.RoundToNearestHour(DateTime.Parse(bed));
            DateTime endTime = job.RoundToNearestHour(DateTime.Parse(end));
            DateTime midnight = DateTime.Parse("12am");

            //Act
            double charge = job.NightlyCharge(startTime, bedTime, midnight, endTime);
            //Assert
            Assert.AreEqual(charge, 92.00);
        }
        [DataTestMethod]
        [DataRow("5pm", "7pm", "3:30am")]
        public void NightlyChargeReturnsCorrectAmountHappy2(string start, string bed, string end)
        {
            //Arrange
            NightJob job = new NightJob();
            DateTime startTime = job.RoundToNearestHour(DateTime.Parse(start));
            DateTime bedTime = job.RoundToNearestHour(DateTime.Parse(bed));
            DateTime endTime = job.RoundToNearestHour(DateTime.Parse(end));
            DateTime midnight = DateTime.Parse("12am");

            //Act
            double charge = job.NightlyCharge(startTime, bedTime, midnight, endTime);
            //Assert
            Assert.AreEqual(charge, 128.00);
        }
    }
}
