using System;
using System.Collections.Generic;
using System.Text;

namespace BabySitterKata
{
    public class NightJob
    {
        public NightJob()
        {

        }
        public NightJob(DateTime startTime, DateTime bedTime, DateTime endTime) // basing the user input to be converted to DateTime before constructing class
        {
            if(DateTime.Compare(DefaultStart, startTime) <= 0 && DateTime.Compare(Midnight, endTime) > 0)// if entered time is after cutoff & before midnight
            {
                this.StartTime = startTime;
            }
            else if(DateTime.Compare(DefaultStart, startTime) <= 0 && DateTime.Compare(Midnight, endTime) <= 0) // if entered time is after cutoff & after midnight
            {
                this.StartTime = startTime.AddDays(1);
            }
            else
            {
                this.StartTime = DefaultStart;
            }
            if(DateTime.Compare(DefaultEnd, endTime)<=0 && DateTime.Compare(Midnight,endTime)<=0)// if entered time is before cuttoff & entered time is after midnight
            {
                this.EndTime = endTime.AddDays(1);
            }
            else if((DateTime.Compare(DefaultEnd, endTime) <= 0 && DateTime.Compare(Midnight, endTime) > 0)) // if enteredtime is before cuttoff & is not after midnight
            {
                this.EndTime = endTime;
            }
            else
            {
                this.EndTime = DefaultEnd;
            }
            this.BedTime = bedTime;
        }
        // if start time and end time are not provided - assigned default times
        public NightJob(DateTime bedTime)
        {
            this.BedTime = bedTime;
        }
        private DateTime DefaultStart { get; set; } = DateTime.Parse("5 PM");
        private  DateTime DefaultEnd { get; set; } = DateTime.Parse("4am").AddDays(1);
        public DateTime StartTime { get; set; }  
        public DateTime Midnight { get; private set; } = DateTime.Parse("12 AM").AddDays(1); // Midnight never changes want it to be immutable (private set)
        public DateTime EndTime { get; set; } 
        public DateTime BedTime { get; set; } = DateTime.Parse("8 PM");

        

        //method for rounding time using DateTime.Minute comparison
        public DateTime RoundToNearestHour(DateTime time)
        {
           
            DateTime roundedTime;
            TimeSpan clearMinutes = new TimeSpan((time.Hour), 0, 0);
            TimeSpan increaseOneHour = new TimeSpan((time.Hour + 1), 0, 0);
            
            if (time.Minute >= 30 && time.Hour == 23)
            {
                DateTime ranToNextDay = time.AddDays(1);
                roundedTime = time.Date + increaseOneHour;
                return roundedTime;

            }
            else if(time.Minute>=30 && time.Hour != 23)
            {
                roundedTime = time.Date + increaseOneHour;
                return roundedTime;
            }
            else
            {

                roundedTime = time.Date + clearMinutes;
            }
            return roundedTime;
        }

        // method for amount made before bedtime - $12
        public double ChargeBeforeBed(DateTime start, DateTime bed)
        {
            return 0;
        }
        
        // method for bedtime to midnight- $8
        public double ChargeFromBedToMidnight(DateTime bedtime, DateTime midnight)
        {
            return 0;
        }
        //method made for amount made before 4AM -$16
        public double ChargeFromMidnightToEnd(DateTime midnight, DateTime endTime)
        {
            return 0;
        }

        //nightly charge method
        public double NightlyCharge(DateTime start, DateTime bed, DateTime midnight, DateTime end)
        {
            return 0;
        }
    }
}
