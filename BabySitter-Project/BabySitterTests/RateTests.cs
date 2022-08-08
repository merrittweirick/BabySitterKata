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
            NightJob testObject = new NightJob(DateTime.Parse(start), DateTime.Parse(bed), DateTime.Parse("4am"));
            
            DateTime startTime =  DateTime.Parse(start);//control
            DateTime bedTime = DateTime.Parse(bed); // control
            NightJob controlObject = new NightJob(startTime, bedTime, DateTime.Parse("4am"));
            
            TimeSpan timespan = controlObject.BedTime - controlObject.StartTime;//control
            int timespanInt = Math.Abs(Convert.ToInt32(timespan.Hours)); // convert to positive int if timespan is negative
            double tester = timespanInt * 12;


            //Act
            double charge = testObject.ChargeBeforeBed();
            //Assert
            Assert.AreEqual(charge, tester); //if bedtime and startime aren't the same time charge should match tester
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
            NightJob testObject = new NightJob(DateTime.Parse("5pm"), DateTime.Parse(bedtime), DateTime.Parse("4am"));
           
            DateTime bedTime = DateTime.Parse(bedtime); // control
            NightJob controlObject = new NightJob(bedTime);
            TimeSpan timespan = controlObject.BedTime- controlObject.Midnight;
            int timespanInt = Convert.ToInt32(timespan.Hours);
            double tester = timespanInt * 8;
            
            //Act
            double charge = testObject.ChargeFromBedToMidnight();
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
            NightJob testObject = new NightJob(DateTime.Parse("5pm"), DateTime.Parse("8pm"), DateTime.Parse(end));

            DateTime endTime = DateTime.Parse(end);//control
            DateTime startTime = DateTime.Parse("5pm");//control
            DateTime bedTime = DateTime.Parse("8pm"); // control
            NightJob controlObject = new NightJob(startTime, bedTime, endTime);// control


            TimeSpan timespan = controlObject.EndTime-controlObject.Midnight;
            int timespanInt = Convert.ToInt32(timespan.Hours);
            double tester = timespanInt * 8;
            //Act
            double charge = testObject.ChargeFromMidnightToEnd();
            //Assert
            Assert.AreEqual(charge, (timespanInt == 0 ? 0.00 : tester));
            Assert.IsNotNull(charge);
        }
        
        [DataTestMethod]
        [DataRow("5pm","8pm","4am")]
        public void NightlyChargeReturnsCorrectAmountHappy(string start, string bed, string end)
        {
            //Arrange
            NightJob testObject = new NightJob(DateTime.Parse(start), DateTime.Parse(bed), DateTime.Parse(end)); 

            //Act
            double charge = testObject.NightlyCharge();
           
            //Assert
            Assert.AreEqual(charge, 132.00);
        }
        [DataTestMethod]
        [DataRow("5pm", "5pm", "12am")] // start and bed are same time end is midnight
        public void NightlyChargeReturnsCorrectAmountEdge1(string start, string bed, string end)
        {
            //Arrange
            NightJob testObject = new NightJob(DateTime.Parse(start), DateTime.Parse(bed), DateTime.Parse(end));

            //Act
            double charge = testObject.NightlyCharge();
           
            //Assert
            Assert.AreEqual(charge, 56.00);
        }
        [DataTestMethod]
        [DataRow("5pm", "5:30pm", "2am")]
        public void NightlyChargeReturnsCorrectAmountEdge2(string start, string bed, string end)
        {
            //Arrange
            NightJob testObject = new NightJob(DateTime.Parse(start), DateTime.Parse(bed), DateTime.Parse(end));

            //Act
            double charge = testObject.NightlyCharge();
           
            //Assert
            Assert.AreEqual(charge, 92.00);
        }
        [DataTestMethod]
        [DataRow("5pm", "7pm", "3:30am")]
        public void NightlyChargeReturnsCorrectAmountHappy2(string start, string bed, string end)
        {
            //Arrange
            NightJob testObject = new NightJob(DateTime.Parse(start), DateTime.Parse(bed), DateTime.Parse(end));
            
            //Act
            double charge = testObject.NightlyCharge();
            
            //Assert
            Assert.AreEqual(charge, 128.00);
        }
    }
}
